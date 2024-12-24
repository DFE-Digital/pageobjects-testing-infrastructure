namespace Dfe.Testing.Pages.Public.Components.GDS.Table.Parts;
public record TableRow
{
    public IEnumerable<TableHeadingItem> Headings { get; init; } = [];
    public IEnumerable<TableDataItem> DataItem { get; init; } = [];
}
