using Dfe.Testing.Pages.Public.Components.GDS.ErrorMessage;
using Dfe.Testing.Pages.Public.Components.Label;

namespace Dfe.Testing.Pages.Public.Components.GDS.Select;
public record GDSSelectComponent : IComponent
{
    public required LabelComponent Label { get; init; }
    public required IEnumerable<OptionComponent> Options { get; init; }
    public string Hint { get; init; } = string.Empty;
    public GDSErrorMessageComponent ErrorMessage { get; init; } = new() { ErrorMessage = string.Empty };
}
