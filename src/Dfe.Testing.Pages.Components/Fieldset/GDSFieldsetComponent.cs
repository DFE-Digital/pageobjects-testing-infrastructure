using Dfe.Testing.Pages.Components.Checkbox;
using Dfe.Testing.Pages.Components.TextInput;
using Dfe.Testing.Pages.Public.DocumentQueryClient;

namespace Dfe.Testing.Pages.Components.Fieldset;

public record GDSFieldsetComponent : IComponent
{
    public required string Legend { get; init; }
    public required IEnumerable<GDSCheckboxComponent> Checkboxes { get; init; }
    public required IEnumerable<GDSTextInputComponent> TextInputs { get; init; }
    public string TagName { get; init; } = "fieldset";
    public string Hint { get; init; } = string.Empty;
}
