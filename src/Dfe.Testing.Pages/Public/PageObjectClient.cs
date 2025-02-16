using Dfe.Testing.Pages.Internal.DocumentClient;

namespace Dfe.Testing.Pages.Public;
// TODO register the "special keys" as configuration that the client can override, expose as defaults through an Options class.
internal interface IPageObjectSchemaMerger
{
    void SetSeed(IEnumerable<PageObjectSchema> seed);
    IEnumerable<PageObjectSchema> Merge(IEnumerable<PageObjectSchema> mergeIn);
}

internal sealed class PageObjectSchemaMerger : IPageObjectSchemaMerger
{
    private List<PageObjectSchema> _seed;
    public PageObjectSchemaMerger(IEnumerable<PageObjectSchema>? seed = null)
    {
        _seed = seed?.ToList() ?? [];
    }

    public void SetSeed(IEnumerable<PageObjectSchema> seed)
    {
        ArgumentNullException.ThrowIfNull(seed);
        _seed = seed.ToList();
    }

    // TODO options for
    // - bool skip if merged is not in template, replace template mapping entirely?
    // 
    public IEnumerable<PageObjectSchema> Merge(IEnumerable<PageObjectSchema> merge)
    {
        List<PageObjectSchema> output = [];
        ArgumentNullException.ThrowIfNull(merge);

        // Where the seed has not had a pageobject.id overridden
        var notInSchemaTemplatePageObjects = _seed
            .Where(templatePageObject => !merge.Any(requestPageObject => requestPageObject.Id == templatePageObject.Id))
            .ToList();

        // Where the client has sent a pageobject.id that is in the template.
        var overriddenToPropertiesForPageObjectInTemplate = merge
            .Where(req => _seed.Any(templatePageObject => templatePageObject.Id == req.Id))
            .Select(req => new PageObjectSchema
            {
                Id = req.Id,
                // Merge mapping from request ontop of the template, if the client has not overridden the mapping, then retain those parts of the template mapping
                Properties = _seed.First(templatePageObject => templatePageObject.Id == req.Id)
                    .Properties
                    .Select(templateMapping => req.Properties
                        .FirstOrDefault(mapping => mapping.ToProperty == templateMapping.ToProperty) ?? templateMapping)
                    .Union(req.Properties
                        .Where(mapping => !_seed.First(templatePageObject => templatePageObject.Id == req.Id)
                            .Properties
                            .Any(templateMapping => templateMapping.ToProperty == mapping.ToProperty)))
                    .ToList(),
                Children = req.Children
            })
            .ToList();

        var schemasThatAreNotPartOfTemplate = merge
            .Where(req => !_seed.Any(templatePageObject => templatePageObject.Id == req.Id))
            .ToList();

        // Add non-overridden template page objects to the output
        output.AddRange(notInSchemaTemplatePageObjects);

        // Add overridden page objects to the output
        output.AddRange(overriddenToPropertiesForPageObjectInTemplate);

        // Add page objects that are not part of the template to the output
        output.AddRange(schemasThatAreNotPartOfTemplate);

        return output.AsEnumerable();
    }
}

internal sealed class PageObjectClient : IPageObjectClient
{
    private readonly IDocumentService _documentService;
    private readonly IPageObjectTemplateFactory _pageObjectTemplateFactory;
    private readonly IPageObjectSchemaMerger _pageObjectTemplateSchemaMerger;

    public PageObjectClient(
        IDocumentService documentService,
        IPageObjectTemplateFactory pageObjectTemplateFactory,
        IPageObjectSchemaMerger pageObjectTemplateSchemaMerger)
    {
        _documentService = documentService;
        _pageObjectTemplateFactory = pageObjectTemplateFactory;
        _pageObjectTemplateSchemaMerger = pageObjectTemplateSchemaMerger;
    }

    public PageObjectResponse Get(PageObjectRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        IPageObjectTemplate? template = null;
        List<PageObjectSchema> outputMergedSchemas = [];

        if (!string.IsNullOrEmpty(request.TemplateId))
        {
            template = _pageObjectTemplateFactory.GetTemplateById(request.TemplateId);

            _pageObjectTemplateSchemaMerger.SetSeed(template.PageObjects);
            var result = _pageObjectTemplateSchemaMerger.Merge(request.PageObjects);

            outputMergedSchemas.AddRange(result);
        }
        else
        {
            outputMergedSchemas.AddRange(request.PageObjects);
        }

        CssElementSelector find = new CssElementSelector(template?.Query?.Find ?? request.Query!.Find ?? throw new ArgumentException("Unable to derive a Find locator"));
        string? scope = template?.Query?.InScope ?? request.Query?.InScope ?? null;


        FindOptions mergedFindOptions = new()
        {
            Find = find,
            InScope = scope != null ? new CssElementSelector(scope) : null
        };


        // Query
        IDocumentSection document = _documentService.ExecuteQuery(mergedFindOptions).Single()
            ?? throw new DocumentSectionNotFoundException(mergedFindOptions.Find, mergedFindOptions.InScope);

        // Mapping
        IList<CreatedPageObjectModel> createdPageModels = outputMergedSchemas?.Select(
            (pageObjectSchema) => new PageObjectModelToCreatedPageObjectModelMapper(document).Map(pageObjectSchema)).ToList() ?? [];

        PageObjectResponse response = new()
        {
            Created = new ReadOnlyCollection<CreatedPageObjectModel>(createdPageModels)
        };

        return response;
    }
}

internal interface IPageObjectTemplateFactory
{
    IPageObjectTemplate GetTemplateById(string templateId);
}

internal sealed class PageObjectTemplateFactory : IPageObjectTemplateFactory
{
    private readonly IEnumerable<IPageObjectTemplate> _pageObjectTemplates;

    public PageObjectTemplateFactory(IEnumerable<IPageObjectTemplate> pageObjectTemplates)
    {
        ArgumentNullException.ThrowIfNull(pageObjectTemplates);
        _pageObjectTemplates = pageObjectTemplates;
    }

    public IPageObjectTemplate GetTemplateById(string templateId)
    {
        ArgumentNullException.ThrowIfNullOrWhiteSpace(templateId);
        IEnumerable<IPageObjectTemplate> templates = _pageObjectTemplates.Where(t => t.TemplateId == templateId);
        if (!templates.Any())
        {
            throw new ArgumentException($"Unable to find template with id: {templateId}");
        }
        if (templates.Count() > 1)
        {
            throw new ArgumentException($"Found multiple templates with id: {templateId}");
        }
        return templates.Single();
    }
}

public interface IPageObjectTemplate
{
    string TemplateId { get; }
    QueryOptions? Query { get; }
    IEnumerable<PageObjectSchema> PageObjects { get; }
}

public class PageObjectRequest
{
    public string? TemplateId { get; set; } = null;
    public QueryOptions? Query { get; set; } = null;
    public IEnumerable<PageObjectSchema> PageObjects { get; set; } = [];
}

public class QueryOptions
{
    public string Find { get; set; } = string.Empty;
    public string? InScope { get; set; } = null!;
}

internal class AttributeResolver
{
    private readonly IDocumentSection _documentSection;

    public AttributeResolver(IDocumentSection documentSection)
    {
        ArgumentNullException.ThrowIfNull(documentSection);
        _documentSection = documentSection;
    }

    public IDictionary<string, string?> ResolveValues(IEnumerable<string> valuesToResolveOnSection)
    {
        return valuesToResolveOnSection.Where(t => !string.IsNullOrEmpty(t))
            .Aggregate(seed: new Dictionary<string, string?>(), (acc, next) =>
            {
                // switch on what the client wants the from to be, if its a "special key" e.g tagname, text, map to the documentSection action. else assume it's an attribute
                acc.Add(next, next switch
                {
                    "text" => _documentSection.Text,
                    "tagname" => _documentSection.TagName,
                    _ => _documentSection.GetAttribute(next),
                });
                return acc;
            });
    }

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
        if (input.Properties == null)
        {
            throw new ArgumentException("Mapping is null");
        }

        // output looks like [ "ToProperty": [ [ { "text": "value" } ], [{"text", "value2"}]]
        Dictionary<string, IEnumerable<IDictionary<string, string?>>> attributes = [];
        input.Properties.ToList().ForEach(options =>
        {
            // TODO infer if selector is a CssSelector or XPath? Put behind an extension so can be tested
            IEnumerable<IDocumentSection> section =
                !string.IsNullOrEmpty(options.MappingEntrypoint) ?
                    _documentSection.FindDescendants(
                        new CssElementSelector(options.MappingEntrypoint)) ?? throw new ArgumentException($"Unable to find mapping entrypoint with {options.MappingEntrypoint} from {_documentSection}")
                    : [_documentSection];

            section.Select((documentSection)
                    => new AttributeResolver(documentSection).ResolveValues(options.Attributes))
                .ToList()
                .ForEach(resolvedValue =>
                {
                    if (attributes.TryGetValue(options.ToProperty, out IEnumerable<IDictionary<string, string?>>? value))
                    {
                        IEnumerable<IDictionary<string, string?>> mergedMappings = value.Append(resolvedValue);
                        attributes[options.ToProperty] = mergedMappings;
                    }
                    else
                    {
                        attributes[options.ToProperty] = [resolvedValue];
                    }
                });
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

        return new CreatedPageObjectModel()
        {
            PageObjectId = input.Id ?? string.Empty,
            Result = new MappingResult()
            {
                Status = MappingStatus.Success,
                Message = "Mapping success"
            },
            PropertyToResolvedAttributes = attributes,
            Children = childPageModels
        };
    }
}

public record PageObjectSchema
{
    public string Id { get; init; } = string.Empty;
    public IEnumerable<PropertyMapping> Properties { get; set; } = null!;
    public IEnumerable<PageObjectSchema> Children { get; set; } = [];
}

public record PropertyMapping
{
    public IEnumerable<string> Attributes { get; set; } = [];
    public string ToProperty { get; set; } = string.Empty;
    public string? MappingEntrypoint { get; set; } = null;
}

// RESPONSE BELOW HERE

public record PageObjectResponse
{
    public IReadOnlyList<CreatedPageObjectModel> Created { get; set; } = [];
}

public record MappingResult
{
    public required MappingStatus Status { get; init; }
    public string Message { get; init; } = string.Empty;
}

public record CreatedPageObjectModel
{
    public string PageObjectId { get; init; } = string.Empty;
    public required MappingResult Result { get; init; }
    // [
    //  "ToProperty" : [
    //      { "text", "1stoutputtedtext" },
    //      { "text",  "2ndoutputtedtext" }}
    //  ]
    public required IDictionary<string, IEnumerable<IDictionary<string, string?>>> PropertyToResolvedAttributes { get; init; }
    public IList<CreatedPageObjectModel> Children { get; init; } = [];
    public IEnumerable<IReadOnlyDictionary<string, string?>> GetMappedProperty(string property)
    {
        ArgumentNullException.ThrowIfNullOrWhiteSpace(property);
        if (PropertyToResolvedAttributes.TryGetValue(property, out var propertyValues))
        {
            return propertyValues.Select(t => new ReadOnlyDictionary<string, string?>(t));
        }
        return [];
    }
}


internal sealed class DocumentSectionNotFoundException : Exception
{
    public DocumentSectionNotFoundException(IElementSelector find, IElementSelector? scope = null)
        : base(message: $"Unable to find document using selector : {find.ToSelector()} with {(scope?.ToSelector() + "document scope") ?? "No document scope"}")
    {
    }
}


public static class CollectionExtensions
{
    public static TOut? TryGetOrDefault<TIn, TOut>(this IEnumerable<KeyValuePair<TIn, TOut>> input, TIn key) where TIn : notnull
    {
        input.ToDictionary(t => t.Key, t => t.Value).TryGetValue(key, out TOut? value);
        return value ?? default;
    }

    public static TOut? TryGetOrDefault<TIn, TOut>(this IDictionary<TIn, TOut> input, TIn key) where TIn : notnull
    {
        input.TryGetValue(key, out TOut? value);
        return value ?? default;
    }
}
