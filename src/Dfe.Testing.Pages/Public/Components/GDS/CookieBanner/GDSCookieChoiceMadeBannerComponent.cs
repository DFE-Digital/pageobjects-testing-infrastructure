using Dfe.Testing.Pages.Public.Components.Core.Link;
using Dfe.Testing.Pages.Public.Components.Core.Text;
using Dfe.Testing.Pages.Public.Components.GDS.Button;

namespace Dfe.Testing.Pages.Public.Components.GDS.CookieBanner;
public record GDSCookieChoiceMadeBannerComponent
{
    public required GDSButtonComponent HideCookies { get; init; }
    public required AnchorLinkComponent ChangeYourCookieSettingsLink { get; init; }
    public required TextComponent Message { get; init; }
}
