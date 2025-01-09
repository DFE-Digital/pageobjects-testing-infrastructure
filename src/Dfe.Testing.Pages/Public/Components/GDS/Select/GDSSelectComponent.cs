using Dfe.Testing.Pages.Public.Components.GDS.ErrorMessage;
using Dfe.Testing.Pages.Public.Components.Label;

namespace Dfe.Testing.Pages.Public.Components.GDS.Select;
public record GDSSelectComponent
{
    public required LabelComponent? Label { get; init; }
    public required IEnumerable<OptionComponent?> Options { get; init; }
    public GDSErrorMessageComponent? ErrorMessage { get; init; } = null;
    public string Hint { get; init; } = string.Empty;
}
