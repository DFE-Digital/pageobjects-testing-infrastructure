using Dfe.Testing.Pages.Public.Components.GDS.Button;
using Dfe.Testing.Pages.Public.Components.Link;

namespace Dfe.Testing.Pages.Public.Components.GDS.CookieBanner;
internal sealed class GDSCookieBannerMapper : IComponentMapper<GDSCookieBannerComponent>
{
    private static readonly CssElementSelector Container = new(".govuk-cookie-banner");
    private readonly ComponentFactory<GDSButtonComponent> _buttonFactory;
    private readonly ComponentFactory<AnchorLinkComponent> _linkFactory;

    public GDSCookieBannerMapper(
        ComponentFactory<GDSButtonComponent> buttonFactory,
        ComponentFactory<AnchorLinkComponent> linkFactory)
    {
        ArgumentNullException.ThrowIfNull(buttonFactory);
        ArgumentNullException.ThrowIfNull(linkFactory);
        _buttonFactory = buttonFactory;
        _linkFactory = linkFactory;
    }
    public GDSCookieBannerComponent Map(IDocumentPart input)
    {
        return new()
        {
            Heading = input.FindDescendant(new CssElementSelector(".govuk-cookie-banner__heading"))!.Text.Trim(),
            //Content = documentPart.GetChild(new CssSelector(".govuk-cookie-banner__content"))!.Text,
            CookieChoiceButtons = _buttonFactory.GetMany(new QueryOptions()
            {
                InScope = Container
            }),
            ViewCookiesLink = _linkFactory.Get(new QueryOptions()
            {
                InScope = Container
            }),
            TagName = input.TagName
        };
    }
}
