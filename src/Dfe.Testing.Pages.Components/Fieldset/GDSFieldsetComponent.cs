using Dfe.Testing.Pages.Components.Inputs.Checkbox;
using Dfe.Testing.Pages.Components.Inputs.Radio;
using Dfe.Testing.Pages.Components.Inputs.TextInput;
using Dfe.Testing.Pages.Components.Select;

namespace Dfe.Testing.Pages.Components.Fieldset;

public record GDSFieldsetComponent : IComponent
{
    public required string Legend { get; init; }
    public IEnumerable<GDSCheckboxComponent> Checkboxes { get; init; } = [];
    public IEnumerable<GDSRadioComponent> Radios { get; init; } = [];
    public IEnumerable<GDSTextInputComponent> TextInputs { get; init; } = [];
    public IEnumerable<GDSSelectComponent> Selects { get; init; } = [];
    public string TagName { get; init; } = "fieldset";
    public string Hint { get; init; } = string.Empty;
}
