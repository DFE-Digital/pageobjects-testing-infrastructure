using Dfe.Testing.Pages.Public.Components.Form;
using Dfe.Testing.Pages.Public.Components.GDS.Button;
using Dfe.Testing.Pages.Public.Components.Link;

namespace Dfe.Testing.Pages.Public.Components.GDS.CookieBanner;
internal sealed class GDSCookieChoiceAvailableBannerComponentBuilder : IGDSCookieChoiceAvailableBannerComponentBuilder
{
    private string _heading = string.Empty;
    private AnchorLinkComponent? anchorLinkComponent = null!;
    private FormComponent? _form;
    private readonly List<GDSButtonComponent> _cookieChoiceButtons = [];
    private readonly IAnchorLinkComponentBuilder _anchorLinkBuilder;
    private readonly IFormBuilder _formBuilder;

    public GDSCookieChoiceAvailableBannerComponentBuilder(
        IAnchorLinkComponentBuilder anchorLink,
        IFormBuilder builder)
    {
        _anchorLinkBuilder = anchorLink;
        _formBuilder = builder;
    }
    public GDSCookieChoiceAvailableBannerComponent Build()
    {
        return new()
        {
            CookieChoiceForm = _form ?? _formBuilder.Build(),
            Heading = new()
            {
                Text = _heading
            },
            ViewCookiesLink = anchorLinkComponent ?? _anchorLinkBuilder.Build()
        };
    }

    public IGDSCookieChoiceAvailableBannerComponentBuilder SetForm(FormComponent form)
    {
        ArgumentNullException.ThrowIfNull(form);
        _form = form;
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
