using Dfe.Testing.Pages.Public.Components.Link;
using Dfe.Testing.Pages.Public.Components.Text;

namespace Dfe.Testing.Pages.Public.Components.GDS.Tabs;
public record GDSTabsComponent
{
    public required IEnumerable<AnchorLinkComponent?> Tabs { get; init; }
    public required TextComponent? Heading { get; init; }
}
