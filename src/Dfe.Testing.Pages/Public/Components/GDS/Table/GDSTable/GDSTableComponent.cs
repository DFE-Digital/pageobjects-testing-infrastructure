using Dfe.Testing.Pages.Public.Components.GDS.Table.TableBody;
using Dfe.Testing.Pages.Public.Components.GDS.Table.TableHead;
using Dfe.Testing.Pages.Public.Components.Text;

namespace Dfe.Testing.Pages.Public.Components.GDS.Table.GDSTable;
public record GDSTableComponent
{
    public required TextComponent? Heading { get; init; }
    public required TableHeadComponent? Head { get; init; }
    public required TableBodyComponent? Body { get; init; }
}
