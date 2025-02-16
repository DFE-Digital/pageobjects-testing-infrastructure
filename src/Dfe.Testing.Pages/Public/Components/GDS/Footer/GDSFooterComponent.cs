using Dfe.Testing.Pages.Public.Components.Text;

namespace Dfe.Testing.Pages.Public.Components.GDS.Footer;
public record GDSFooterComponent
{
    public required TextComponent? LicenseMessage { get; init; }
    public required IEnumerable<AnchorLinkComponentOld?> ApplicationLinks { get; init; } = [];
    public required AnchorLinkComponentOld? LicenseLink { get; init; }
    public required AnchorLinkComponentOld? CrownCopyrightLink { get; init; }
}
