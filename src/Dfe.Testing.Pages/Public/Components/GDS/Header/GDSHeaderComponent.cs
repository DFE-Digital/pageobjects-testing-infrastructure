using Dfe.Testing.Pages.Public.Components.Link;

namespace Dfe.Testing.Pages.Public.Components.GDS.Header;
public record GDSHeaderComponent
{
    public required AnchorLinkComponent? GovUKLink { get; init; }
    public IEnumerable<AnchorLinkComponent?> NavigationLinks { get; init; } = [];
}
