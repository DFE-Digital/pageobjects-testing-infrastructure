using Dfe.Testing.Pages.Internal.DocumentClient;

namespace Dfe.Testing.Pages.Public;
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

        IEnumerable<AttributeMappingResult> results = input.Mapping.Select(options =>
        {
            // TODO figure out if the selector is a CssSelector or XPath? Put behind an extension so can be tested
            IDocumentSection section =
                !string.IsNullOrEmpty(options.MappingEntrypoint) ?
                    _documentSection.FindDescendant(new CssElementSelector(options.MappingEntrypoint)) ?? throw new ArgumentException($"Unable to find mapping entrypoint with {options.MappingEntrypoint} from {_documentSection}")
                    : _documentSection;

            var resolver = new DocumentSectionValuesResolver(
                section,
                options.ToProperty,
                options.Values);

            var resolvedAttributes = resolver.ResolveValues();

            // TODO handle if the client tries to map multiple properties to the same PropertyOutKey
            /*if (attributes.TryGetValue(resolvedAttributes.Key, out IEnumerable<KeyValuePair<string, string>>? existingValues))
            {
                attributes[resolvedAttributes.Key] = attributes[resolvedAttributes.Key].Concat(existingValues);
            }
            else
            {
                attributes[resolvedAttributes.Key] = resolvedAttributes.Value;
            }*/

            return new AttributeMappingResult()
            {
                Message = "Mapping success",
                Context = section.Document.ToString(),
                Status = MappingStatus.Success,
                Attributes = resolvedAttributes
            };
        });

        List<CreatedPageObjectModel> childPageModels = [];

        // recursively handle children
        IEnumerator<PageObjectSchema> childIterator = input.Children.GetEnumerator();

        while (childIterator.MoveNext())
        {
            // recurse
            CreatedPageObjectModel model =
                new PageObjectModelToCreatedPageObjectModelMapper(_documentSection)
                .Map(childIterator.Current);

            childPageModels.Add(model);
        }

        return new CreatedPageObjectModel(
            input.Id ?? string.Empty,
            results,
            childPageModels);
    }
}

public record PageObjectSchema
{
    public string Id { get; } = string.Empty;
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

public record AttributeMappingResult
{
    public string Message { get; init; } = string.Empty;
    public required string Context { get; init; }
    public required MappingStatus Status { get; init; }
    public required KeyValuePair<string, IEnumerable<KeyValuePair<string, string>>> Attributes { get; init; }
}

public record CreatedPageObjectModel(
    string Id,
    IEnumerable<AttributeMappingResult> Results,
    IList<CreatedPageObjectModel> Children);


internal sealed class DocumentSectionNotFoundException : Exception
{
    public DocumentSectionNotFoundException(IElementSelector find, IElementSelector? scope = null)
        : base(message: $"Unable to find document using selector : {find.ToSelector()} with {(scope?.ToSelector() + "document scope") ?? "No document scope"}")
    {
    }
}
