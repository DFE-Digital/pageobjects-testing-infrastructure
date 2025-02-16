using Dfe.Testing.Pages.Public.Components.Link;

namespace Dfe.Testing.Pages.Public.Components.GDS.Header;
public record GDSHeaderComponent
{
    public required AnchorLinkComponentOld? GovUKLink { get; init; }
    public IEnumerable<AnchorLinkComponentOld?> NavigationLinks { get; init; } = [];
}
