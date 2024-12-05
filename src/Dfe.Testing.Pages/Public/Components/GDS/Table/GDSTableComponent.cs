namespace Dfe.Testing.Pages.Public.Components.GDS.Table;
public record GDSTableComponent : IComponent
{
    public required string Heading { get; init; }
    public TableHead Head { get; init; } = new TableHead() { Rows = [] };
    public required TableBody Body { get; init; }
}
