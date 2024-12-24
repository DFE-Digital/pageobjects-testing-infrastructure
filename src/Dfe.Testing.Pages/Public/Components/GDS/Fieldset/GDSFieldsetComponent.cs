using Dfe.Testing.Pages.Public.Components.Core.Text;
using Dfe.Testing.Pages.Public.Components.GDS.Checkbox;
using Dfe.Testing.Pages.Public.Components.GDS.Radio;
using Dfe.Testing.Pages.Public.Components.GDS.TextInput;

namespace Dfe.Testing.Pages.Public.Components.GDS.Fieldset;

public record GDSFieldsetComponent
{
    public required TextComponent Legend { get; init; }
    public IEnumerable<GDSCheckboxComponent> Checkboxes { get; init; } = [];
    public IEnumerable<GDSRadioComponent> Radios { get; init; } = [];
    public IEnumerable<GDSTextInputComponent> TextInputs { get; init; } = [];
    public IEnumerable<GDSSelectComponent> Selects { get; init; } = [];
    public string TagName { get; init; } = "fieldset";
    public string Hint { get; init; } = string.Empty;
}
