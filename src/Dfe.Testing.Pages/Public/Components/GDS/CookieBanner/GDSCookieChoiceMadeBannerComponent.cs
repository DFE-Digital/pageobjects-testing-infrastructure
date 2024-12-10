using Dfe.Testing.Pages.Public.Components.GDS.Button;
using Dfe.Testing.Pages.Public.Components.Link;
using Dfe.Testing.Pages.Public.Components.Text;

namespace Dfe.Testing.Pages.Public.Components.GDS.CookieBanner;
public record GDSCookieChoiceMadeBannerComponent : IComponent
{
    public required GDSButtonComponent HideCookies { get; init; }
    public required AnchorLinkComponent ChangeYourCookieSettingsLink { get; init; }
    public required TextComponent Message { get; init; }
}
