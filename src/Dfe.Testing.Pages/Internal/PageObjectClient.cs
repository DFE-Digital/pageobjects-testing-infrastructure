using Dfe.Testing.Pages.Public.PageObjects;
using Dfe.Testing.Pages.Public.PageObjects.Documents;
using Dfe.Testing.Pages.Public.PageObjects.PageObjectClient;
using Dfe.Testing.Pages.Public.PageObjects.PageObjectClient.Request;
using Dfe.Testing.Pages.Public.PageObjects.PageObjectClient.Response;
using Dfe.Testing.Pages.Public.PageObjects.Selector;

namespace Dfe.Testing.Pages.Internal;

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

        var scope = request.Query?.InScope ?? null;
        FindOptions mergedFindOptions = new()
        {
            Find = new CssElementSelector(request.Query?.Find ?? "html"),
            InScope = scope == null ? null : new CssElementSelector(scope)
        };

        // Query
        IDocumentSection document = _documentService.ExecuteQuery(mergedFindOptions).Single()
            ?? throw new DocumentSectionNotFoundException(mergedFindOptions.Find, mergedFindOptions.InScope);

        var outputCreatedPageModels =
            // Expand all Sections->Schema from PageObjectSchema.Find
            request.MapSchemas
                .Select(pageObjectSchema =>
                    (document.FindAllDocumentSectionsForPageObjectSchema(pageObjectSchema), pageObjectSchema))
                // Mapping
                .SelectMany((sectionsToPageObjectSchema) =>
                {
                    (var sections, var schema) = sectionsToPageObjectSchema;
                    return sections.Select(section => new PageObjectModelToCreatedPageObjectModelMapper(section).Map(schema));
                })
                .ToList()
                .AsReadOnly();

        PageObjectResponse response = new()
        {
            Created = outputCreatedPageModels
        };

        return response;
    }
}

internal sealed class PageObjectModelToCreatedPageObjectModelMapper : IMapper<PageObjectSchema, CreatedPageObjectModel>
{
    private readonly IDocumentSection _documentSection;

    public PageObjectModelToCreatedPageObjectModelMapper(IDocumentSection documentSection)
    {
        ArgumentNullException.ThrowIfNull(documentSection);
        _documentSection = documentSection;
    }

    public CreatedPageObjectModel Map(PageObjectSchema input)
    {
        // recursively handle children
        List<CreatedPageObjectModel> outputChildPageModels = [];
        var childIterator = input.Children.GetEnumerator();

        while (childIterator.MoveNext())
        {
            var childModels =
                _documentSection.FindAllDocumentSectionsForPageObjectSchema(childIterator.Current)
                    .Select((section) =>
                        new PageObjectModelToCreatedPageObjectModelMapper(section).Map(childIterator.Current));

            outputChildPageModels.AddRange(childModels);
        }

        IDictionary<string, string?> attributes = _documentSection.Attributes.ToDictionary();
        attributes.TryAdd("text", _documentSection.Text);

        return new CreatedPageObjectModel()
        {
            Id = input.Id ?? string.Empty,
            Results = attributes,
            // TODO this may not be successful
            Status = new PageObjectSchemaResponse()
            {
                Status = MappingStatus.Success,
                Message = "Mapping success"
            },
            Children = outputChildPageModels
        };
    }
}

// RESPONSE BELOW HERE
internal sealed class DocumentSectionNotFoundException : Exception
{
    public DocumentSectionNotFoundException(IElementSelector find, IElementSelector? scope = null)
        : base(message: $"Unable to find document using selector : {find.ToSelector()} with {scope?.ToSelector() + "document scope" ?? "No document scope"}")
    {
    }
}

public static class DocumentSectionExtensions
{
    public static IEnumerable<IDocumentSection> FindAllDocumentSectionsForPageObjectSchema(this IDocumentSection section, PageObjectSchema schema)
    {
        IEnumerable<IDocumentSection>? sections = string.IsNullOrEmpty(schema.Find) ?
            [section] :
                section.FindDescendants(new CssElementSelector(schema.Find));
        return sections ?? [];
    }
}
