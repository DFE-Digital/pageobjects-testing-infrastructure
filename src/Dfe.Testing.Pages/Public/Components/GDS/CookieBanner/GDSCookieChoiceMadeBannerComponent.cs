using Dfe.Testing.Pages.Public.Components.Form;
using Dfe.Testing.Pages.Public.Components.Link;
using Dfe.Testing.Pages.Public.Components.Text;

namespace Dfe.Testing.Pages.Public.Components.GDS.CookieBanner;
public record GDSCookieChoiceMadeBannerComponent
{
    internal GDSCookieChoiceMadeBannerComponent() { }
    public required FormComponent? HideCookiesForm { get; init; }
    public required AnchorLinkComponent? ChangeYourCookieSettingsLink { get; init; }
    public required TextComponent? Message { get; init; }
}

public interface IGDSCookieChoiceMadeBannerComponentBuilder
{
    IGDSCookieChoiceMadeBannerComponentBuilder SetForm(FormComponent form);
    IGDSCookieChoiceMadeBannerComponentBuilder SetChangeYourCookieSettingsLink(AnchorLinkComponent link);
    IGDSCookieChoiceMadeBannerComponentBuilder SetMessage(string message);
    GDSCookieChoiceMadeBannerComponent Build();
}
