namespace Dfe.Testing.Pages.Public.Components.GDS.Table.Parts;
public record TableHead : IComponent
{
    public required IEnumerable<TableRow> Rows { get; init; }
}
