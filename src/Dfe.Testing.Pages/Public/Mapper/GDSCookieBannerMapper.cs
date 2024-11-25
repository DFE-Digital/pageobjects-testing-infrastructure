using Dfe.Testing.Pages.Components.Button;
using Dfe.Testing.Pages.Components.CookieBanner;
using Dfe.Testing.Pages.Public.Mapper.Interface;

namespace Dfe.Testing.Pages.Public.Mapper;
internal sealed class GDSCookieBannerMapper : IComponentMapper<GDSCookieBannerComponent>
{
    private static readonly CssSelector Container = new(".govuk-cookie-banner");
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
            Heading = input.GetChild(new CssSelector(".govuk-cookie-banner__heading"))!.Text.Trim(),
            //Content = documentPart.GetChild(new CssSelector(".govuk-cookie-banner__content"))!.Text,
            CookieChoiceButtons = _buttonFactory.GetMany(new QueryRequestArgs()
            {
                Scope = Container
            }),
            ViewCookiesLink = _linkFactory.Get(new QueryRequestArgs()
            {
                Scope = Container
            }),
            TagName = input.TagName
        };
    }
}
