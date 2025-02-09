using System.Linq;
using Dfe.Testing.Pages.Internal.DocumentClient;
using Dfe.Testing.Pages.Public.Components.EntrypointSelectorFactory;
using Microsoft.Extensions.Options;

namespace Dfe.Testing.Pages.Public.Components;

public interface IComponentFactory<T> where T : class
{
    CreatedComponentResponse<T> Create(CreateComponentRequest? request = null);
    IEnumerable<CreatedComponentResponse<T>> CreateMany(CreateComponentRequest? request = null);
}

internal sealed class ComponentFactory<TComponent> : IComponentFactory<TComponent> where TComponent : class
{
    private readonly IDocumentService _documentClient;
    private readonly IComponentMapper<TComponent> _mapper;
    private readonly IEntrypointSelectorFactory _componentSelectorFactory;

    public ComponentFactory(
        IDocumentService documentClient,
        IComponentMapper<TComponent> mapper,
        IEntrypointSelectorFactory componentSelectorFactory)
    {
        _documentClient = documentClient;
        _mapper = mapper;
        _componentSelectorFactory = componentSelectorFactory;
    }

    public CreatedComponentResponse<TComponent> Create(CreateComponentRequest? request = null) => CreateMany(request).Single();

    public IEnumerable<CreatedComponentResponse<TComponent>> CreateMany(CreateComponentRequest? request = null)
    {
        FindOptions mergedFindOptions = new()
        {
            InScope = request?.FindInScope ?? null,
            Find = request?.Selector ?? _componentSelectorFactory.GetSelector<TComponent>()
        };

        // Query
        IEnumerable<IDocumentSection> documentSections = _documentClient.ExecuteQuery(mergedFindOptions) ?? [];


        // Map
        IEnumerable<MappedResponse<TComponent>> mappedComponentResponsesFromDocumentSections =
            documentSections.Select((section) =>
            {
                ArgumentNullException.ThrowIfNull(section, $"section returned as null in DocumentClient query");
                string componentRequestedType = typeof(TComponent).Name;
                DocumentSectionMapRequest mapRequest = new()
                {
                    Document = section,
                    MappedResults = [],
                    Options = new()
                    {
                        // adds requested component as type to fulfil MapConfiguration override
                        // e.g {TopLevelComponent e.g FormComponent}{Separator}{Attribute}{Separator}{Attribute} is structure of key looked up in mappers
                        // e.g FormComponent.ViewCookiesLink.Text - TextMapper will use current ChainedLookupKey using the attribute it's currently mapping. This could be a top level map / a nested map.
                        MapKey = new MapKey([componentRequestedType]),
                        OverrideMapperConfiguration = request?.Mapping ?? [],
                        // top level entrypoint not changed as Query finds section
                        OverrideMapperEntrypoint = null,
                    }
                };
                MappedResponse<TComponent> response = _mapper.Map(mapRequest);
                return response;
            });

        // Generate CreatedComponentResponse
        return mappedComponentResponsesFromDocumentSections.Select(
            (mappedResponse) =>
            {
                List<IMappingResult> outputResults = [];
                outputResults.AddRange(mappedResponse.MappingResults);

                return new CreatedComponentResponse<TComponent>()
                {
                    Created = mappedResponse.Mapped!,
                    CreatingComponentResults = outputResults.AsReadOnly()
                };
            });
    }
}

public interface IPageObjectClient
{
    PageObjectResponse Get(PageObjectRequest request);
    // TODO should there be a builder that lets the client create a PageObjectRequest given it's complex ... where client doesn't want to handle as JSON?
    // should we take in the schema, with JsonSerialiser overload - or force client to Serialise to model? if client wants YAML?
}

internal sealed class PageObjectClient : IPageObjectClient
{
    private readonly IDocumentService _documentService;

    public PageObjectClient(IDocumentService documentService)
    {
        _documentService = documentService;
    }

    public PageObjectResponse Get(PageObjectRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);
        // Query
        ArgumentNullException.ThrowIfNull(request.Query);
        ArgumentNullException.ThrowIfNull(request.Query.Find);

        FindOptions mergedFindOptions = new()
        {
            InScope = new CssElementSelector(request.Query?.InScope ?? string.Empty),
            Find = new CssElementSelector(request.Query!.Find)
        };

        // Query
        IDocumentSection document = _documentService.ExecuteQuery(mergedFindOptions).Single() ?? throw new DocumentSectionNotFoundException(mergedFindOptions.Find, mergedFindOptions.InScope);

        // Mapping
        PageObjectModelToCreatedPageObjectModelMapper mapper = new(document);
        IList<CreatedPageObjectModel> createdPageModels = request.PageObject?.Select(mapper.Map).ToList() ?? [];

        PageObjectResponse response = new()
        {
            Created = new ReadOnlyCollection<CreatedPageObjectModel>(createdPageModels)
        };

        return response;
    }
}

internal sealed class DocumentSectionNotFoundException : Exception
{
    public DocumentSectionNotFoundException(IElementSelector find, IElementSelector? scope = null)
        : base(message: $"Unable to find document using selector : {find.ToSelector()} with {(scope?.ToSelector() + "document scope") ?? "No document scope"}")
    {
    }
}



public class PageObjectRequest
{
    public QueryOptions Query { get; set; } = null!;
    public IEnumerable<PageObjectSchema> PageObject { get; set; } = [];
}

public class QueryOptions
{
    public string Find { get; set; } = string.Empty;
    public string? InScope { get; set; } = null!;
}



internal class DocumentSectionValuesResolver
{
    const string DefaultMappedAttributesOutputKey = "_"; // TODO default key configuration
    private readonly IDocumentSection _documentSection;
    private readonly string _property;
    private readonly IEnumerable<string> _valuesToResolve;

    public DocumentSectionValuesResolver(
        IDocumentSection documentSection,
        string property,
        IEnumerable<string> valuesToResolve)
    {
        ArgumentNullException.ThrowIfNull(documentSection);
        _valuesToResolve = valuesToResolve ?? [];
        _documentSection = documentSection;
        _property = property ?? DefaultMappedAttributesOutputKey;
        _valuesToResolve = valuesToResolve ?? [];
    }

    public KeyValuePair<string, IEnumerable<KeyValuePair<string, string>>> ResolveValues()
        => KeyValuePair.Create(
            key: _property,
            value: _valuesToResolve.Where(t => !string.IsNullOrEmpty(t))
            .Select(valueToResolve
                // switch on what the client wants the from to be, if its a "special key" e.g tagname, text, map to the documentSection action. else assume it's an attribute
                => new KeyValuePair<string, string>(
                    key: valueToResolve,
                    value: valueToResolve switch
                    {
                        "text" => _documentSection.Text,
                        "tagname" => _documentSection.TagName,
                        _ => _documentSection.GetAttribute(valueToResolve) ?? string.Empty,
                    })));

}

public sealed class PageObjectModelToCreatedPageObjectModelMapper : IMapper<PageObjectSchema, CreatedPageObjectModel>
{
    private readonly IDocumentSection _documentSection;

    public PageObjectModelToCreatedPageObjectModelMapper(IDocumentSection documentSection)
    {
        ArgumentNullException.ThrowIfNull(documentSection);
        _documentSection = documentSection;
    }
    public CreatedPageObjectModel Map(PageObjectSchema input)
    {
        if (input.Mapping == null)
        {
            throw new ArgumentException("Mapping is null");
        }

        Dictionary<string, IEnumerable<KeyValuePair<string, string>>> mappedPageObjectAttributes = input.Mapping.Select(options =>
        {
            // TODO figure out if the selector is a CssSelector or XPath? Put behind an extension so can be tested
            IDocumentSection section =
                !string.IsNullOrEmpty(options.MappingEntrypoint) ?
                    _documentSection.FindDescendant(new CssElementSelector(options.MappingEntrypoint)) ?? throw new ArgumentException($"Unable to find mapping entrypoint with {options.MappingEntrypoint} from {_documentSection}")
                    : _documentSection;

            return new DocumentSectionValuesResolver(
                section,
                options.ToProperty,
                options.Values);
        })
        .Select(t => t.ResolveValues())
        .Aggregate(new Dictionary<string, IEnumerable<KeyValuePair<string, string>>>(), (acc, next) =>
        {
            // If client maps multiple items same ToProperty e.g "Heading" then append how. TODO COULD THIS CONFLICT IF @text or @id is used
            if (acc.TryGetValue(next.Key, out IEnumerable<KeyValuePair<string, string>>? existingValues))
            {
                acc[next.Key] = next.Value.Concat(existingValues);
            }
            else
            {
                acc[next.Key] = next.Value;
            }
            return acc;
        });


        CreatedPageObjectModel mappedOutput = new()
        {
            MappedAttributes = mappedPageObjectAttributes
        };

        // recursively handle children
        IEnumerator<PageObjectSchema> childIterator = input.Children.GetEnumerator();

        while (childIterator.MoveNext())
        {
            // recurse
            CreatedPageObjectModel model =
                new PageObjectModelToCreatedPageObjectModelMapper(_documentSection)
                .Map(childIterator.Current);

            mappedOutput.Children.Add(model);
        }

        return mappedOutput;
    }
}

public record PageObjectSchema
{
    public string Identifier { get; } = string.Empty;
    public IEnumerable<AttributeMapping> Mapping { get; set; } = null!;
    public IEnumerable<PageObjectSchema> Children { get; set; } = [];
}

public record AttributeMapping // TODO could be complex e.g from: "" or from : { with options in here, aggregating, operating on }
{
    public IEnumerable<string> Values { get; set; } = [];
    public string ToProperty { get; set; } = string.Empty;
    public string? MappingEntrypoint { get; set; } = null;
}


public record PageObjectResponse
{
    public IReadOnlyList<CreatedPageObjectModel> Created { get; set; } = [];
}

public interface IPageObjectMappingResult
{
    string Context { get; }
    MappingStatus Status { get; }
    string Message { get; }
}

public record CreatedPageObjectModel
{
    // TODO make MappedAttributes change the KeyValuePair<string, string> to include public IList<IPageObjectMappingResult> Results { get; init; } = []; based on the success/fail of every attributeMap attempt
    // Maybe a model called AttributeMappingResult with a success/fail, value? and a message
    public Dictionary<string, IEnumerable<KeyValuePair<string, string>>> MappedAttributes { get; init; } = [];
    public IList<CreatedPageObjectModel> Children { get; init; } = [];
}

public sealed class SuccessfulAttributeMappingResult : IPageObjectMappingResult
{
    public SuccessfulAttributeMappingResult(string context)
    {
        if (string.IsNullOrEmpty(context))
        {
            throw new ArgumentNullException(nameof(context));
        }
        Context = context;
    }

    public string Context { get; }
    public MappingStatus Status { get => MappingStatus.Success; }
    public string Message { get => "Mapping successful"; }
}

public sealed class FailedAttributeMappingResult : IPageObjectMappingResult
{
    public FailedAttributeMappingResult(string context, string message)
    {
        if (string.IsNullOrEmpty(context))
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (string.IsNullOrEmpty(message))
        {
            throw new ArgumentNullException(nameof(message));
        }
        Context = context;
        Message = message;
    }
    public string Context { get; }
    public MappingStatus Status { get => MappingStatus.Failed; }
    public string Message { get; }
}
