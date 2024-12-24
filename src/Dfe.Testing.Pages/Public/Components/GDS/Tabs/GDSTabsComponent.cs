using Dfe.Testing.Pages.Public.Components.Core.Link;
using Dfe.Testing.Pages.Public.Components.Core.Text;

namespace Dfe.Testing.Pages.Public.Components.GDS.Tabs;
public record GDSTabsComponent
{
    public required IEnumerable<AnchorLinkComponent> Tabs { get; init; }
    public required TextComponent Heading { get; init; } = new() { Text = string.Empty };
}
