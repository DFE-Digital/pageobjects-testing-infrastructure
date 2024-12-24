namespace Dfe.Testing.Pages.Public.Components.GDS.Table.Parts;
public record TableBody
{
    public required IEnumerable<TableRow> Rows { get; init; }
}
