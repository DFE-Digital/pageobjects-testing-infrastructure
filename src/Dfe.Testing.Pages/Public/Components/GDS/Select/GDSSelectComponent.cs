using Dfe.Testing.Pages.Public.Components.Core.Label;
using Dfe.Testing.Pages.Public.Components.GDS.ErrorMessage;

namespace Dfe.Testing.Pages.Public.Components.GDS.Select;
public record GDSSelectComponent
{
    public required LabelComponent Label { get; init; }
    public required IEnumerable<OptionComponent> Options { get; init; }
    public string Hint { get; init; } = string.Empty;
    public GDSErrorMessageComponent ErrorMessage { get; init; } = new() { ErrorMessage = new() { Text = string.Empty } };
}
