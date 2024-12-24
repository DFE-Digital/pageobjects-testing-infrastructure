using Dfe.Testing.Pages.Public.Components.Core.Link;
using Dfe.Testing.Pages.Public.Components.Core.Text;
using Dfe.Testing.Pages.Public.Components.GDS.Button;

namespace Dfe.Testing.Pages.Public.Components.GDS.CookieBanner;
public record GDSCookieChoiceAvailableBannerComponent
{
    public required TextComponent Heading { get; init; }
    public required IEnumerable<GDSButtonComponent> CookieChoiceButtons { get; init; }
    public required AnchorLinkComponent ViewCookiesLink { get; init; }
}
