namespace Dfe.Testing.Pages.Components.Table;
public record TableHeadingItem : IComponent
{
    public required string Text { get; init; }
    public string Scope { get; init; } = string.Empty;
}
