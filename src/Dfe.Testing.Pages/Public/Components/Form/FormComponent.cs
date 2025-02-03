using Dfe.Testing.Pages.Public.Components.GDS.Button;
using Dfe.Testing.Pages.Public.Components.GDS.Checkbox;
using Dfe.Testing.Pages.Public.Components.GDS.Fieldset;
using Dfe.Testing.Pages.Public.Components.GDS.Radio;
using Dfe.Testing.Pages.Public.Components.GDS.TextInput;
using Dfe.Testing.Pages.Public.Components.HiddenInput;
using HttpMethod = System.Net.Http.HttpMethod;

namespace Dfe.Testing.Pages.Public.Components.Form;

public record FormComponent
{
    internal FormComponent() { }
    public required HttpMethod Method { get; init; }
    public string Action { get; init; } = string.Empty;
    public bool IsFormValidated { get; init; } = true;
    public required IEnumerable<GDSFieldsetComponent?> Fieldsets { get; init; } = [];
    public required IEnumerable<GDSButtonComponent?> Buttons { get; init; } = [];
    public IEnumerable<GDSCheckboxComponent?> Checkboxes { get; init; } = [];
    public IEnumerable<GDSRadioComponent?> Radios { get; init; } = [];
    public IEnumerable<GDSTextInputComponent?> TextInputs { get; init; } = [];
    public IEnumerable<GDSSelectComponent?> Selects { get; init; } = [];
    public IEnumerable<HiddenInputComponent?> HiddenInputs { get; init; } = [];
}

public interface IFormBuilder
{
    FormComponent Build();
    public IFormBuilder SetMethod(HttpMethod method);
    public IFormBuilder SetMethod(string method);
    public IFormBuilder SetAction(string action);
    public IFormBuilder SetIsFormHTMLValidated(bool validated);
    public IFormBuilder AddButton(Action<IGDSButtonBuilder>? configureButton = null);
    public IFormBuilder AddButton(GDSButtonComponent button);
    public IFormBuilder AddCheckbox(Action<IGDSCheckboxBuilder>? confiureCheckbox = null);
    public IFormBuilder AddCheckbox(GDSCheckboxComponent checkbox);
}
