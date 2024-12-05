namespace Dfe.Testing.Pages.Public.Components.GDS.Table.Parts;
public record TableRow : IComponent
{
    public IEnumerable<TableHeadingItem> Headings { get; init; } = [];
    public IEnumerable<TableDataItem> DataItem { get; init; } = [];
}
