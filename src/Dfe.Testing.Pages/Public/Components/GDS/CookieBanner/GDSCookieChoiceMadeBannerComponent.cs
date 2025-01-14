using Dfe.Testing.Pages.Public.Components.GDS.Button;
using Dfe.Testing.Pages.Public.Components.Link;
using Dfe.Testing.Pages.Public.Components.Text;

namespace Dfe.Testing.Pages.Public.Components.GDS.CookieBanner;
public record GDSCookieChoiceMadeBannerComponent
{
    internal GDSCookieChoiceMadeBannerComponent() { }
    public required GDSButtonComponent? HideCookies { get; init; }
    public required AnchorLinkComponent? ChangeYourCookieSettingsLink { get; init; }
    public required TextComponent? Message { get; init; }
}

public interface IGDSCookieChoiceMadeBannerComponentBuilder
{
    IGDSCookieChoiceMadeBannerComponentBuilder SetHideCookiesButton(GDSButtonComponent button);
    IGDSCookieChoiceMadeBannerComponentBuilder SetChangeYourCookieSettingsLink(AnchorLinkComponent link);
    IGDSCookieChoiceMadeBannerComponentBuilder SetMessage(string message);
    GDSCookieChoiceMadeBannerComponent Build();
}

internal sealed class GDSCookieChoiceMadeBannerComponentBuilder : IGDSCookieChoiceMadeBannerComponentBuilder
{
    private string _message = string.Empty;
    private GDSButtonComponent? _button = null;
    private AnchorLinkComponent? _link = null;
    public GDSCookieChoiceMadeBannerComponent Build()
        => new()
        {
            ChangeYourCookieSettingsLink = _link,
            HideCookies = _button,
            Message = new()
            {
                Text = _message
            }
        };

    public IGDSCookieChoiceMadeBannerComponentBuilder SetHideCookiesButton(GDSButtonComponent button)
    {
        ArgumentNullException.ThrowIfNull(button);
        _button = button;
        return this;
    }

    public IGDSCookieChoiceMadeBannerComponentBuilder SetChangeYourCookieSettingsLink(AnchorLinkComponent link)
    {
        ArgumentNullException.ThrowIfNull(link);
        _link = link;
        return this;
    }

    public IGDSCookieChoiceMadeBannerComponentBuilder SetMessage(string message)
    {
        ArgumentNullException.ThrowIfNull(message);
        _message = message;
        return this;
    }
}
