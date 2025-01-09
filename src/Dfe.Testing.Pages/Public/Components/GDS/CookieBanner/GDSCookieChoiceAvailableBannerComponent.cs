using Dfe.Testing.Pages.Public.Components.GDS.Button;
using Dfe.Testing.Pages.Public.Components.Link;
using Dfe.Testing.Pages.Public.Components.Text;

namespace Dfe.Testing.Pages.Public.Components.GDS.CookieBanner;
public record GDSCookieChoiceAvailableBannerComponent
{
    internal GDSCookieChoiceAvailableBannerComponent() { }
    public required TextComponent? Heading { get; init; }
    public required IEnumerable<GDSButtonComponent?> CookieChoiceButtons { get; init; }
    public required AnchorLinkComponent? ViewCookiesLink { get; init; }
}

public interface IGDSCookieChoiceAvailableBannerComponentBuilder
{
    GDSCookieChoiceAvailableBannerComponent Build();
    IGDSCookieChoiceAvailableBannerComponentBuilder SetHeading(string heading);
    IGDSCookieChoiceAvailableBannerComponentBuilder AddCookieChoiceButton(GDSButtonComponent button);
    IGDSCookieChoiceAvailableBannerComponentBuilder SetViewCookiesLink(AnchorLinkComponent link);
}
