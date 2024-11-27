using Dfe.Testing.Pages.Components.Inputs.Checkbox;
using Dfe.Testing.Pages.Components.Inputs.Radio;
using Dfe.Testing.Pages.Components.Inputs.TextInput;

namespace Dfe.Testing.Pages.Components.Fieldset;

public record GDSFieldsetComponent : IComponent
{
    public required string Legend { get; init; }
    public required IEnumerable<GDSCheckboxComponent> Checkboxes { get; init; }
    public required IEnumerable<GDSRadioComponent> Radios { get; init; }
    public required IEnumerable<GDSTextInputComponent> TextInputs { get; init; }
    public string TagName { get; init; } = "fieldset";
    public string Hint { get; init; } = string.Empty;
}
