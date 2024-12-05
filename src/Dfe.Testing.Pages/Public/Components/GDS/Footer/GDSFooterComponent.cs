using Dfe.Testing.Pages.Public.Components.Link;

namespace Dfe.Testing.Pages.Public.Components.GDS.Footer;
public record GDSFooterComponent : IComponent
{
    public required string LicenseMessage { get; init; }
    public IEnumerable<AnchorLinkComponent> ApplicationLinks { get; init; } = [];
    public required AnchorLinkComponent LicenseLink { get; init; }
    public required AnchorLinkComponent CrownCopyrightLink { get; init; }
}
