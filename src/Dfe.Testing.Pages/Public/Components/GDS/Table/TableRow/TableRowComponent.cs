using Dfe.Testing.Pages.Public.Components.GDS.Table.TableDataItem;
using Dfe.Testing.Pages.Public.Components.GDS.Table.TableHeadingItem;

namespace Dfe.Testing.Pages.Public.Components.GDS.Table.TableRow;
public record TableRowComponent
{
    public IEnumerable<TableHeadingItemComponent> Headings { get; init; } = [];
    public IEnumerable<TableDataItemComponent> DataItems { get; init; } = [];
}
