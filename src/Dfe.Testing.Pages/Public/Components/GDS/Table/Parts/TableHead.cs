namespace Dfe.Testing.Pages.Public.Components.GDS.Table.Parts;
public record TableHead
{
    public required IEnumerable<TableRow> Rows { get; init; }
}
