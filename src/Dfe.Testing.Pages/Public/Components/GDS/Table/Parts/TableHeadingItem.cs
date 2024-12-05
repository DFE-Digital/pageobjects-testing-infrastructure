namespace Dfe.Testing.Pages.Public.Components.GDS.Table.Parts;
public record TableHeadingItem : IComponent
{
    public required string Text { get; init; }
    public string Scope { get; init; } = string.Empty;
}
