using Dfe.Testing.Pages.Public.Components.Form;
using Dfe.Testing.Pages.Public.Components.Link;

namespace Dfe.Testing.Pages.Public.Components.GDS.CookieBanner;
internal sealed class GDSCookieChoiceMadeBannerComponentBuilder : IGDSCookieChoiceMadeBannerComponentBuilder
{
    private string _message = string.Empty;
    private FormComponent? _form = null;
    private AnchorLinkComponent? _link = null;
    public GDSCookieChoiceMadeBannerComponent Build()
        => new()
        {
            ChangeYourCookieSettingsLink = _link,
            HideCookiesForm = _form,
            Message = new()
            {
                Text = _message
            }
        };

    public IGDSCookieChoiceMadeBannerComponentBuilder SetForm(FormComponent form)
    {
        ArgumentNullException.ThrowIfNull(form);
        _form = form;
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
