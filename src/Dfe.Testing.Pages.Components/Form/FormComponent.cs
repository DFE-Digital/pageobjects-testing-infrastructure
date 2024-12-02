using Dfe.Testing.Pages.Components.Button;
using Dfe.Testing.Pages.Components.Fieldset;
using Dfe.Testing.Pages.Components.Inputs;
using Dfe.Testing.Pages.Components.Inputs.Checkbox;
using Dfe.Testing.Pages.Components.Inputs.Radio;
using Dfe.Testing.Pages.Components.Inputs.TextInput;
using Dfe.Testing.Pages.Components.Select;
using HttpMethod = System.Net.Http.HttpMethod;

namespace Dfe.Testing.Pages.Components.Form;

public record FormComponent : IComponent
{
    public required HttpMethod Method { get; init; }
    public string Action { get; init; } = string.Empty;
    public required IEnumerable<GDSFieldsetComponent> FieldSets { get; init; } = [];
    public required IEnumerable<GDSButtonComponent> Buttons { get; init; } = [];
    public IEnumerable<GDSCheckboxComponent> Checkboxes { get; init; } = [];
    public IEnumerable<GDSRadioComponent> Radios { get; init; } = [];
    public IEnumerable<GDSTextInputComponent> TextInputs { get; init; } = [];
    public IEnumerable<GDSSelectComponent> Selects { get; init; } = [];
    public IEnumerable<HiddenInputComponent> HiddenInputs { get; init; } = [];
    public required bool IsFormValidatedWithHTML { get; init; }
}
