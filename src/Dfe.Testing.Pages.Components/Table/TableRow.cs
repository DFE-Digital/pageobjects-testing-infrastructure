namespace Dfe.Testing.Pages.Components.Table;
public record TableRow : IComponent
{
    public IEnumerable<TableHeadingItem> Headings { get; init; } = [];
    public IEnumerable<TableDataItem> DataItem { get; init; } = [];
}
