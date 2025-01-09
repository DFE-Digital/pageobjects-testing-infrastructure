using Dfe.Testing.Pages.Public.Components.GDS.Table.TableRow;

namespace Dfe.Testing.Pages.Public.Components.GDS.Table.TableHead;
public record TableHeadComponent
{
    public required IEnumerable<TableRowComponent> Rows { get; init; }
}
