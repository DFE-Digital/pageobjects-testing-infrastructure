namespace Dfe.Testing.Pages.Components.Table;
public record GDSTableComponent : IComponent
{
    public required string Heading { get; init; }
    public TableHead Head { get; init; } = new TableHead() { Rows = [] };
    public required TableBody Body { get; init; }
}
