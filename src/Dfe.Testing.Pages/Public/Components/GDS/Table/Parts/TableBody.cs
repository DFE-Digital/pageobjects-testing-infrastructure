namespace Dfe.Testing.Pages.Public.Components.GDS.Table.Parts;
public record TableBody : IComponent
{
    public required IEnumerable<TableRow> Rows { get; init; }
}
