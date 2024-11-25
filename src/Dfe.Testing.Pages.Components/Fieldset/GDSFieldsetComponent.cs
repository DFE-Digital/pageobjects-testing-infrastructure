using Dfe.Testing.Pages.Components.Checkbox;
using Dfe.Testing.Pages.Public.DocumentQueryClient;

namespace Dfe.Testing.Pages.Components.Fieldset;

public record GDSFieldsetComponent : IComponent
{
    public required string TagName { get; init; }
    public required string Legend { get; init; }
    public required IEnumerable<GDSCheckboxWithLabelComponent> Checkboxes { get; init; }
}
