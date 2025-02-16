using Dfe.Testing.Pages.Public.Components.Form;
using Dfe.Testing.Pages.Public.Components.Link;
using Dfe.Testing.Pages.Public.Components.Text;

namespace Dfe.Testing.Pages.Public.Components.GDS.CookieBanner;
public record GDSCookieChoiceAvailableBannerComponent
{
    internal GDSCookieChoiceAvailableBannerComponent() { }
    public required TextComponent? Heading { get; init; }
    public required FormComponentOld CookieChoiceForm { get; init; }
    public required AnchorLinkComponentOld? ViewCookiesLink { get; init; }
}

public interface IGDSCookieChoiceAvailableBannerComponentBuilder
{
    GDSCookieChoiceAvailableBannerComponent Build();
    IGDSCookieChoiceAvailableBannerComponentBuilder SetHeading(string heading);
    IGDSCookieChoiceAvailableBannerComponentBuilder SetViewCookiesLink(AnchorLinkComponentOld link);
    IGDSCookieChoiceAvailableBannerComponentBuilder SetForm(FormComponentOld form);
}
