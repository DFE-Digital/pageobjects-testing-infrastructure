using Dfe.Testing.Pages.Public.Components.Core.Link;
using Dfe.Testing.Pages.Public.Components.Core.Text;

namespace Dfe.Testing.Pages.Public.Components.GDS.Footer;
public record GDSFooterComponent
{
    public required TextComponent LicenseMessage { get; init; }
    public IEnumerable<AnchorLinkComponent> ApplicationLinks { get; init; } = [];
    public required AnchorLinkComponent LicenseLink { get; init; }
    public required AnchorLinkComponent CrownCopyrightLink { get; init; }
}
