namespace Dfe.Testing.Pages.Components.Select;
public record OptionComponent : IComponent
{
    public required string Value { get; init; }
    public required string Text { get; init; }
    public bool IsSelected { get; init; } = false;
}
