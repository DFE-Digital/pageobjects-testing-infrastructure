using Dfe.Testing.Pages.Public.Components.Form;
using Dfe.Testing.Pages.Public.Components.Link;
using Dfe.Testing.Pages.Public.Components.Text;

namespace Dfe.Testing.Pages.Public.Components.GDS.CookieBanner;
public record GDSCookieChoiceMadeBannerComponent
{
    internal GDSCookieChoiceMadeBannerComponent() { }
    public required FormComponentOld? HideCookiesForm { get; init; }
    public required AnchorLinkComponentOld? ChangeYourCookieSettingsLink { get; init; }
    public required TextComponent? Message { get; init; }
}

public interface IGDSCookieChoiceMadeBannerComponentBuilder
{
    IGDSCookieChoiceMadeBannerComponentBuilder SetForm(FormComponentOld form);
    IGDSCookieChoiceMadeBannerComponentBuilder SetChangeYourCookieSettingsLink(AnchorLinkComponentOld link);
    IGDSCookieChoiceMadeBannerComponentBuilder SetMessage(string message);
    GDSCookieChoiceMadeBannerComponent Build();
}
