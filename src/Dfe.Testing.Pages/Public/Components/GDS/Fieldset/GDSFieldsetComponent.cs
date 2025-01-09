using Dfe.Testing.Pages.Public.Components.GDS.Checkbox;
using Dfe.Testing.Pages.Public.Components.GDS.Radio;
using Dfe.Testing.Pages.Public.Components.GDS.TextInput;
using Dfe.Testing.Pages.Public.Components.Text;

namespace Dfe.Testing.Pages.Public.Components.GDS.Fieldset;

public record GDSFieldsetComponent
{
    public TextComponent? Legend { get; init; }
    public IEnumerable<GDSCheckboxComponent?> Checkboxes { get; init; } = [];
    public IEnumerable<GDSRadioComponent?> Radios { get; init; } = [];
    public IEnumerable<GDSTextInputComponent?> TextInputs { get; init; } = [];
    public IEnumerable<GDSSelectComponent?> Selects { get; init; } = [];
    public string Hint { get; init; } = string.Empty;
}
