
using System.ComponentModel;
using Dfe.Testing.Pages.Public.Components.GDS.Button;
using Dfe.Testing.Pages.Public.Components.Link;
using Dfe.Testing.Pages.Public.Components.Text;

namespace Dfe.Testing.Pages.Public.Components.GDS.CookieBanner;
internal sealed class GDSCookieChoiceMadeBannerMappper : IComponentMapper<GDSCookieChoiceMadeBannerComponent>
{
    private static readonly CssElementSelector Container = new(".govuk-cookie-banner");
    private readonly ComponentFactory<TextComponent> _textFactory;
    private readonly ComponentFactory<AnchorLinkComponent> _linkFactory;
    private readonly ComponentFactory<GDSButtonComponent> _buttonFactory;

    public GDSCookieChoiceMadeBannerMappper(
        ComponentFactory<TextComponent> textFactory,
        ComponentFactory<AnchorLinkComponent> linkFactory,
        ComponentFactory<GDSButtonComponent> buttonFactory)
    {
        _textFactory = textFactory;
        _linkFactory = linkFactory;
        _buttonFactory = buttonFactory;
    }
    public GDSCookieChoiceMadeBannerComponent Map(IDocumentPart input)
    {
        return new()
        {
            Message = _textFactory.Get(new QueryOptions()
            {
                Query = new CssElementSelector(".govuk-cookie-banner_content"),
                InScope = Container
            }),
            ChangeYourCookieSettingsLink = _linkFactory.Get(new QueryOptions()
            {
                InScope = Container
            }),
            HideCookies = _buttonFactory.Get(new QueryOptions()
            {
                InScope = Container
            })
        };
    }
}
