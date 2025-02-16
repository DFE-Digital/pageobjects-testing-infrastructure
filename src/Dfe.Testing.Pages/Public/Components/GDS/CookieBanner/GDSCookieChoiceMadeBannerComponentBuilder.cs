using Dfe.Testing.Pages.Public.Components.Form;
using Dfe.Testing.Pages.Public.Components.Link;

namespace Dfe.Testing.Pages.Public.Components.GDS.CookieBanner;
internal sealed class GDSCookieChoiceMadeBannerComponentBuilder : IGDSCookieChoiceMadeBannerComponentBuilder
{
    private string _message = string.Empty;
    private FormComponentOld? _form = null;
    private AnchorLinkComponentOld? _link = null;
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

    public IGDSCookieChoiceMadeBannerComponentBuilder SetForm(FormComponentOld form)
    {
        ArgumentNullException.ThrowIfNull(form);
        _form = form;
        return this;
    }

    public IGDSCookieChoiceMadeBannerComponentBuilder SetChangeYourCookieSettingsLink(AnchorLinkComponentOld link)
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
