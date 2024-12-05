using Dfe.Testing.Pages.Public.Components.Link;

namespace Dfe.Testing.Pages.Public.Components.GDS.Tabs;
public record GDSTabsComponent : IComponent
{
    public required IEnumerable<AnchorLinkComponent> Tabs { get; init; }
    public string Heading { get; init; } = string.Empty;
    public string TagName { get; init; } = string.Empty;
}
