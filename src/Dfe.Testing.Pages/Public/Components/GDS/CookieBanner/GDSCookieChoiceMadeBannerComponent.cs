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
    IGDSCookieChoiceMadeBannerComponentBuilder SetHideCookiesButton(Action<IGDSButtonBuilder> configureButton);
    IGDSCookieChoiceMadeBannerComponentBuilder SetChangeYourCookieSettingsLink(AnchorLinkComponent link);
    IGDSCookieChoiceMadeBannerComponentBuilder SetChangeYourCookieSettingsLink(Action<IAnchorLinkComponentBuilder> configureLink);
    IGDSCookieChoiceMadeBannerComponentBuilder SetMessage(string message);
    GDSCookieChoiceMadeBannerComponent Build();
}

internal sealed class GDSCookieChoiceMadeBannerComponentBuilder : IGDSCookieChoiceMadeBannerComponentBuilder
{
    private readonly IGDSButtonBuilder _buttonBuilder;
    private readonly IAnchorLinkComponentBuilder _linkBuilder;
    private string _message = string.Empty;

    public GDSCookieChoiceMadeBannerComponentBuilder(
        IGDSButtonBuilder buttonBuilder,
        IAnchorLinkComponentBuilder linkBuilder)
    {
        _buttonBuilder = buttonBuilder;
        _linkBuilder = linkBuilder;
    }
    public GDSCookieChoiceMadeBannerComponent Build()
        => new()
        {
            ChangeYourCookieSettingsLink = _linkBuilder.Build(),
            HideCookies = _buttonBuilder.Build(),
            Message = new()
            {
                Text = _message
            }
        };

    public IGDSCookieChoiceMadeBannerComponentBuilder SetHideCookiesButton(GDSButtonComponent button)
    {
        ArgumentNullException.ThrowIfNull(button);
        _buttonBuilder.SetButtonStyle(button.ButtonStyle)
            .SetEnabled(button.IsEnabled)
            .SetName(button.Name)
            .SetValue(button.Value)
            .SetType(button.Type)
            .SetText(button.Text.Text);
        return this;
    }

    public IGDSCookieChoiceMadeBannerComponentBuilder SetChangeYourCookieSettingsLink(AnchorLinkComponent link)
    {
        ArgumentNullException.ThrowIfNull(link);
        _linkBuilder.SetLink(link.Link)
            .SetOpensInNewTab(link.OpensInNewTab)
            .SetText(link.Text!.Text);
        return this;
    }

    public IGDSCookieChoiceMadeBannerComponentBuilder SetChangeYourCookieSettingsLink(Action<IAnchorLinkComponentBuilder> configureLink)
    {
        ArgumentNullException.ThrowIfNull(configureLink);
        configureLink(_linkBuilder);
        return this;
    }

    public IGDSCookieChoiceMadeBannerComponentBuilder SetMessage(string message)
    {
        ArgumentNullException.ThrowIfNull(message);
        _message = message;
        return this;
    }

    public IGDSCookieChoiceMadeBannerComponentBuilder SetHideCookiesButton(Action<IGDSButtonBuilder> configureButton)
    {
        ArgumentNullException.ThrowIfNull(configureButton);
        configureButton(_buttonBuilder);
        return this;
    }
}
