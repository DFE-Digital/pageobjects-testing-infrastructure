using Dfe.Testing.Pages.Public.Components.GDS.Button;
using Dfe.Testing.Pages.Public.Components.Link;
using Dfe.Testing.Pages.Public.Components.Text;

namespace Dfe.Testing.Pages.Public.Components.GDS.CookieBanner;
internal sealed class GDSCookieChoiceAvailableMapper : IComponentMapper<GDSCookieChoiceAvailableBannerComponent>
{
    private static readonly CssElementSelector Container = new(".govuk-cookie-banner");
    private readonly ComponentFactory<GDSButtonComponent> _buttonFactory;
    private readonly ComponentFactory<AnchorLinkComponent> _linkFactory;
    private readonly ComponentFactory<TextComponent> _textFactory;

    public GDSCookieChoiceAvailableMapper(
        ComponentFactory<GDSButtonComponent> buttonFactory,
        ComponentFactory<AnchorLinkComponent> linkFactory,
        ComponentFactory<TextComponent> textFactory)
    {
        ArgumentNullException.ThrowIfNull(buttonFactory);
        ArgumentNullException.ThrowIfNull(linkFactory);
        ArgumentNullException.ThrowIfNull(textFactory);
        _buttonFactory = buttonFactory;
        _linkFactory = linkFactory;
        _textFactory = textFactory;
    }
    public GDSCookieChoiceAvailableBannerComponent Map(IDocumentPart input)
    {
        return new()
        {
            Heading = _textFactory.Get(new QueryOptions()
            {
                Query = new CssElementSelector(".govuk-cookie-banner__heading"),
                InScope = Container
            }),
            CookieChoiceButtons = _buttonFactory.GetMany(new QueryOptions()
            {
                InScope = Container
            }),
            ViewCookiesLink = _linkFactory.Get(new QueryOptions()
            {
                InScope = Container
            })
        };
    }
}
