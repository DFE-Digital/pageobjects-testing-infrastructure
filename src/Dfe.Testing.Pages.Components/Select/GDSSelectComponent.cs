using Dfe.Testing.Pages.Components.ErrorMessage;
using Dfe.Testing.Pages.Components.Label;

namespace Dfe.Testing.Pages.Components.Select;
public record GDSSelectComponent : IComponent
{
    public required LabelComponent Label { get; init; }
    public required IEnumerable<OptionComponent> Options { get; init; }
    public string Hint { get; init; } = string.Empty;
    public GDSErrorMessageComponent ErrorMessage { get; init; } = new() { ErrorMessage = string.Empty };
}

public record OptionComponent : IComponent
{
    public required string Value { get; init; }
    public required string Text { get; init; }
    public bool IsSelected { get; init; } = false;
}
