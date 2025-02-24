using Dfe.Testing.Pages.Public.PageObjects;
using Dfe.Testing.Pages.Public.PageObjects.PageObjectClient.Request;
using Dfe.Testing.Pages.Public.PageObjects.PageObjectClient.Response;
using Dfe.Testing.Pages.Public.PageObjects.PageObjectClient.Templates;

namespace Dfe.Testing.Pages.Components.Form;
public record FormComponent
{
    public HttpMethod? Method { get; init; }
    public string? Action { get; init; }
    public IEnumerable<ButtonComponent> Buttons { get; init; } = [];
    public IEnumerable<InputComponent> Inputs { get; init; } = [];
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
    private readonly IMapper<CreatedPageObjectModel, InputComponent> _inputMapper;

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

    public FormTemplate(
        FormPageOptions formOptions)
    {
        _formOptions = formOptions;
    }

    public string Id => nameof(FormComponent);
    public PageObjectSchema Schema => new()
    {
        Id = _formOptions.Form,
        Find = "form",
        Children = [
            new InputComponentTemplate(_formOptions.Inputs).Schema,
            new PageObjectSchema()
            {
                Id = _formOptions.Buttons,
                Find = "button"
            }
        ]
    };
}
