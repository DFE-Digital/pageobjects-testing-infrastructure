using Dfe.Testing.Pages.Internal.DocumentClient;
using Dfe.Testing.Pages.Public.Templates;
using HttpMethod = System.Net.Http.HttpMethod;

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

        string? scope = request.Query?.InScope ?? null;
        FindOptions mergedFindOptions = new()
        {
            Find = new CssElementSelector(request.Query?.Find ?? "html"),
            InScope = scope == null ? null : new CssElementSelector(scope)
        };

        // Query
        IDocumentSection document = _documentService.ExecuteQuery(mergedFindOptions).Single()
            ?? throw new DocumentSectionNotFoundException(mergedFindOptions.Find, mergedFindOptions.InScope);

        ReadOnlyCollection<CreatedPageObjectModel> outputCreatedPageModels =
            // Expand all Sections->Schema from PageObjectSchema.Find
            request.MapSchemas
                .Select(pageObjectSchema =>
                    (document.FindAllDocumentSectionsForPageObjectSchema(pageObjectSchema), pageObjectSchema))
                // Mapping
                .SelectMany((sectionsToPageObjectSchema) =>
                {
                    (IEnumerable<IDocumentSection> sections, PageObjectSchema schema) = sectionsToPageObjectSchema;
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

public class PageObjectRequest
{
    public DocumentQueryOptions? Query { get; set; } = null;
    public IEnumerable<PageObjectSchema> MapSchemas { get; set; } = [];
}

public class DocumentQueryOptions
{
    public string Find { get; set; } = string.Empty;
    public string? InScope { get; set; } = null!;
}

public record PageObjectSchema
{
    public string Id { get; set; } = string.Empty;
    public string Find { get; set; } = string.Empty;
    public IEnumerable<PageObjectSchema> Children { get; set; } = [];
}

public interface IPageObjectTemplateFactory
{
    IPageObjectTemplate GetTemplateById(string templateId);
    IPageObjectTemplate GetTemplateForType<T>() where T : class;
    IPageObjectTemplate GetTemplateForType(Type componentType);
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
        IEnumerable<IPageObjectTemplate> templates = _pageObjectTemplates.Where(t => t.Id == templateId);
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

    public IPageObjectTemplate GetTemplateForType<T>() where T : class => GetTemplateForType(typeof(T));

    public IPageObjectTemplate GetTemplateForType(Type componentType) => GetTemplateById(componentType.Name);
}

// TODO consider Target<T> as an extension of this. Then it registers its type, and that's all handled via DI.

public interface IPageObjectTemplate
{
    string Id { get; }
    PageObjectSchema Schema { get; }
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
        // recursively handle children
        List<CreatedPageObjectModel> outputChildPageModels = [];
        IEnumerator<PageObjectSchema> childIterator = input.Children.GetEnumerator();

        while (childIterator.MoveNext())
        {
            IEnumerable<CreatedPageObjectModel> childModels =
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
            Status = new MappingResult()
            {
                Status = MappingStatus.Success,
                Message = "Mapping success"
            },
            Children = outputChildPageModels
        };
    }
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
    public string Id { get; init; } = string.Empty;
    public required MappingResult Status { get; init; } // query failed, mapping failed, mapping success
    public IDictionary<string, string?> Results { get; init; } = new Dictionary<string, string?>();
    public IList<CreatedPageObjectModel> Children { get; init; } = [];
    public string? GetAttribute(string attribute) => Results.TryGetOrDefault(attribute);
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

public record InputComponent
{
    public LabelComponent? Label { get; init; }
    public string? Id { get; init; }
    public string? Name { get; init; }
    public string? Value { get; init; }
    public string? Type { get; init; }
}


public sealed class InputMapper : IMapper<CreatedPageObjectModel, InputComponent>
{
    private readonly InputComponentOptions _inputOptions;

    public InputMapper(InputComponentOptions inputOptions)
    {
        _inputOptions = inputOptions;
    }
    public InputComponent Map(CreatedPageObjectModel mapFrom)
    {
        CreatedPageObjectModel input = mapFrom.Children.Single(t => t.Id == _inputOptions.Input);
        CreatedPageObjectModel label = mapFrom.Children.Single(t => t.Id == _inputOptions.Label);

        return new InputComponent()
        {

            Label = new()
            {
                For = label.GetAttribute("for"),
                Text = label.GetAttribute("text")
            },
            Id = input.GetAttribute("id"),
            Name = input.GetAttribute("name"),
            Type = input.GetAttribute("type"),
            Value = input.GetAttribute("value")
        };
    }
}

public sealed class InputComponentTemplate : IPageObjectTemplate
{
    private readonly InputComponentOptions _options;

    public InputComponentTemplate(
        InputComponentOptions options)
    {
        _options = options;
    }

    public string Id => nameof(InputComponent);
    public PageObjectSchema Schema => new()
    {
        Id = _options.Container,
        Find = "div:has(input)",
        Children = [
            new PageObjectSchema()
            {
                Id = _options.Input,
                Find = "input"
            },
            new PageObjectSchema()
            {
                Id = _options.Label,
                Find = "label"
            }
        ]
    };
}

public sealed class InputComponentOptions
{
    public string Container { get; set; } = "InputContainer";
    public string Input { get; set; } = "InputContainer.Input";
    public string Label { get; set; } = "InputContainer.Label";
}

public sealed class FormPageOptions
{
    private const string ROOT = "Form";
    private readonly InputComponentOptions _inputOptions;

    public FormPageOptions(
        InputComponentOptions inputOptions)
    {
        _inputOptions = inputOptions;
    }

    public string Form => ROOT;
    public string Buttons => $"{ROOT}.Buttons";
    public InputComponentOptions Inputs { get => _inputOptions; }

}


public sealed class FormNewMapper : IMapper<CreatedPageObjectModel, FormComponent>
{
    private readonly FormPageOptions _options;
    private readonly IMapper<CreatedPageObjectModel, ButtonComponent> _buttonMapper;
    private readonly IMapper<CreatedPageObjectModel, Public.InputComponent> _inputMapper;

    public FormNewMapper(
        FormPageOptions options,
        IMapper<CreatedPageObjectModel, ButtonComponent> buttonMapper,
        IMapper<CreatedPageObjectModel, InputComponent> inputMapper)
    {
        _buttonMapper = buttonMapper;
        _inputMapper = inputMapper;
        _options = options;
    }

    public FormComponent Map(CreatedPageObjectModel input)
    {
        return new()
        {
            Action = input.GetAttribute("action"),
            Method = input.GetAttribute("method") is null ? null : HttpMethod.Parse(input.GetAttribute("method")),
            Buttons = input.Children.Where(t => t.Id == _options.Buttons).Select(_buttonMapper.Map),
            Inputs = input.Children.Where(t => t.Id == _options.Inputs.Container).Select(_inputMapper.Map)
        };
    }
}

public sealed class FormTemplate : IPageObjectTemplate
{
    private readonly FormPageOptions _formOptions;
    private readonly InputComponentOptions _inputOptions;

    public FormTemplate(
        FormPageOptions formOptions,
        InputComponentOptions inputOptions)
    {
        _formOptions = formOptions;
        _inputOptions = inputOptions;
    }

    public string Id => nameof(FormComponent);
    public PageObjectSchema Schema => new()
    {
        Id = _formOptions.Form,
        Find = "form",
        Children = [
            new InputComponentTemplate(_inputOptions).Schema,
            new PageObjectSchema()
            {
                Id = _formOptions.Buttons,
                Find = "button"
            }
        ]
    };
}
