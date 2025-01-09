using Dfe.Testing.Pages.Public.Components.Checkbox;
using Dfe.Testing.Pages.Public.Components.GDS.ErrorMessage;
using Dfe.Testing.Pages.Public.Components.Label;

namespace Dfe.Testing.Pages.Public.Components.GDS.Checkbox;

public sealed record GDSCheckboxComponent
{
    public required CheckboxComponent? Checkbox { get; init; }
    public required LabelComponent? Label { get; init; }
    public required GDSErrorMessageComponent? Error { get; init; }
    public bool IsRequired { get; init; } = false;
}
