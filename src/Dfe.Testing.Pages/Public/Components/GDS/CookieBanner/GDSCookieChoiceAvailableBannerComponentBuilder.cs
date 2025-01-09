using Dfe.Testing.Pages.Public.Components.GDS.Button;
using Dfe.Testing.Pages.Public.Components.Link;

namespace Dfe.Testing.Pages.Public.Components.GDS.CookieBanner;
internal sealed class GDSCookieChoiceAvailableBannerComponentBuilder : IGDSCookieChoiceAvailableBannerComponentBuilder
{
    private string _heading = string.Empty;
    private AnchorLinkComponent anchorLinkComponent = null!;
    private readonly List<GDSButtonComponent> _cookieChoiceButtons = [];
    private readonly IAnchorLinkComponentBuilder _anchorLinkBuilder;

    public GDSCookieChoiceAvailableBannerComponentBuilder(IAnchorLinkComponentBuilder anchorLink)
    {
        _anchorLinkBuilder = anchorLink;
    }
    public GDSCookieChoiceAvailableBannerComponent Build()
    {
        return new()
        {
            CookieChoiceButtons = _cookieChoiceButtons,
            Heading = new()
            {
                Text = _heading
            },
            ViewCookiesLink = anchorLinkComponent ?? _anchorLinkBuilder.Build()
        };
    }


    public IGDSCookieChoiceAvailableBannerComponentBuilder AddCookieChoiceButton(GDSButtonComponent button)
    {
        ArgumentNullException.ThrowIfNull(button);
        _cookieChoiceButtons.Add(button);
        return this;
    }

    public IGDSCookieChoiceAvailableBannerComponentBuilder SetViewCookiesLink(AnchorLinkComponent link)
    {
        ArgumentNullException.ThrowIfNull(link);
        anchorLinkComponent = link;
        return this;
    }

    public IGDSCookieChoiceAvailableBannerComponentBuilder SetHeading(string heading)
    {
        ArgumentNullException.ThrowIfNull(heading);
        _heading = heading;
        return this;
    }
}
