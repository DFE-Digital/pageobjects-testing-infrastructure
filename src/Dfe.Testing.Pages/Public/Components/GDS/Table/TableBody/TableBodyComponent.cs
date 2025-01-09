using Dfe.Testing.Pages.Public.Components.GDS.Table.TableRow;

namespace Dfe.Testing.Pages.Public.Components.GDS.Table.TableBody;
public record TableBodyComponent
{
    public required IEnumerable<TableRowComponent> Rows { get; init; }
}
