using Dfe.Testing.Pages.Public.DocumentQueryClient.Pages.Components;

namespace Dfe.Testing.Pages.Internal.ComponentFactory;
internal sealed class GDSCookieBannerComponentFactory : ComponentFactory<GDSCookieBanner>
{
    private static readonly CssSelector Container = new(".govuk-cookie-banner");
    private readonly ComponentFactory<GDSButton> _buttonFactory;
    private readonly ComponentFactory<AnchorLink> _anchorLinkFactory;

    private Func<IDocumentPart, GDSCookieBanner> MapToCookieBanner =>
        (documentPart)
            => new GDSCookieBanner
            {
                Heading = documentPart.GetChild(new CssSelector(".govuk-cookie-banner__heading"))!.Text.Trim(),
                //Content = documentPart.GetChild(new CssSelector(".govuk-cookie-banner__content"))!.Text,
                CookieChoiceButtons = _buttonFactory.GetMany(new QueryRequestArgs()
                {
                    Scope = Container
                }),
                ViewCookiesLink = _anchorLinkFactory.Get(new QueryRequestArgs()
                {
                    Scope = Container
                }),
                TagName = documentPart.TagName
            };
    public GDSCookieBannerComponentFactory(
        IDocumentQueryClientAccessor documentQueryClientAccessor,
        ComponentFactory<GDSButton> buttonFactory,
        ComponentFactory<AnchorLink> anchorLinkFactory) : base(documentQueryClientAccessor)
    {
        ArgumentNullException.ThrowIfNull(buttonFactory);
        ArgumentNullException.ThrowIfNull(anchorLinkFactory);
        _buttonFactory = buttonFactory;
        _anchorLinkFactory = anchorLinkFactory;
    }

    public override List<GDSCookieBanner> GetMany(QueryRequestArgs? request = null)
        => DocumentQueryClient.QueryMany<GDSCookieBanner>(
                args: MergeRequest(request, Container),
                mapper: MapToCookieBanner)
            .ToList();
}
