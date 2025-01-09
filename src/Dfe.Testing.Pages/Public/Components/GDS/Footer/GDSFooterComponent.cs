using Dfe.Testing.Pages.Public.Components.Link;
using Dfe.Testing.Pages.Public.Components.Text;

namespace Dfe.Testing.Pages.Public.Components.GDS.Footer;
public record GDSFooterComponent
{
    public required TextComponent? LicenseMessage { get; init; }
    public required IEnumerable<AnchorLinkComponent?> ApplicationLinks { get; init; } = [];
    public required AnchorLinkComponent? LicenseLink { get; init; }
    public required AnchorLinkComponent? CrownCopyrightLink { get; init; }
}
