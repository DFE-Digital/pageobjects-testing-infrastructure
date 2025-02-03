using Dfe.Testing.Pages.Public.Components.GDS.Button;
using Dfe.Testing.Pages.Public.Components.GDS.Checkbox;
using HttpMethod = System.Net.Http.HttpMethod;

namespace Dfe.Testing.Pages.Public.Components.Form;
internal sealed class FormBuilder : IFormBuilder
{
    private string _action = string.Empty;
    private HttpMethod _method = HttpMethod.Get;
    private bool _isFormValidated = true;
    private readonly IGDSButtonBuilder _buttonBuilder;
    private readonly List<GDSButtonComponent> _buttons = [];

    public FormBuilder(IGDSButtonBuilder buttonBuilder)
    {
        _buttonBuilder = buttonBuilder;
    }
    public FormComponent Build()
    {
        return new()
        {
            Action = _action,
            Method = _method,
            Buttons = _buttons,
            Fieldsets = [],
            Checkboxes = [],
            Radios = [],
            TextInputs = [],
            HiddenInputs = [],
            Selects = []
        };
    }

    public IFormBuilder AddButton(GDSButtonComponent button)
    {
        ArgumentNullException.ThrowIfNull(button);
        _buttons.Add(button);
        return this;
    }

    public IFormBuilder AddButton(Action<IGDSButtonBuilder>? configureButton = null)
    {
        configureButton?.Invoke(_buttonBuilder);
        GDSButtonComponent button = _buttonBuilder.Build();
        _buttons.Add(button);
        return this;
    }

    public IFormBuilder SetAction(string action)
    {
        ArgumentNullException.ThrowIfNull(action);
        _action = action;
        return this;
    }

    public IFormBuilder SetIsFormHTMLValidated(bool validated)
    {
        _isFormValidated = validated;
        return this;
    }

    public IFormBuilder SetMethod(HttpMethod method)
    {
        ArgumentNullException.ThrowIfNull(method);
        _method = method;
        return this;
    }

    public IFormBuilder SetMethod(string method)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(method);
        _method = HttpMethod.Parse(method);
        return this;
    }

    public IFormBuilder AddCheckbox(Action<IGDSCheckboxBuilder>? confiureCheckbox = null)
    {
        throw new NotImplementedException();
    }

    public IFormBuilder AddCheckbox(GDSCheckboxComponent checkbox)
    {
        throw new NotImplementedException();
    }
}
