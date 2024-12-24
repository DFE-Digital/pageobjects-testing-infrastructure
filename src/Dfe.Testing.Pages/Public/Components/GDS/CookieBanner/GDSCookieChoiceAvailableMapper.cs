using Dfe.Testing.Pages.Public.Components.Core.Link;
using Dfe.Testing.Pages.Public.Components.Core.Text;
using Dfe.Testing.Pages.Public.Components.GDS.Button;

namespace Dfe.Testing.Pages.Public.Components.GDS.CookieBanner;
internal sealed class GDSCookieChoiceAvailableMapper : BaseDocumentSectionToComponentMapper<GDSCookieChoiceAvailableBannerComponent>
{
    private readonly IMapper<IDocumentSection, GDSButtonComponent> _buttonMapper;
    private readonly IMapper<IDocumentSection, AnchorLinkComponent> _linkMapper;
    private readonly IMapper<IDocumentSection, TextComponent> _textMapper;

    public GDSCookieChoiceAvailableMapper(
        IDocumentSectionFinder documentSectionFinder,
        IMapper<IDocumentSection, GDSButtonComponent> buttonMapper,
        IMapper<IDocumentSection, AnchorLinkComponent> linkMapper,
        IMapper<IDocumentSection, TextComponent> textMapper) : base(documentSectionFinder)
    {
        ArgumentNullException.ThrowIfNull(buttonMapper);
        ArgumentNullException.ThrowIfNull(linkMapper);
        ArgumentNullException.ThrowIfNull(textMapper);
        _buttonMapper = buttonMapper;
        _linkMapper = linkMapper;
        _textMapper = textMapper;
    }

    public override GDSCookieChoiceAvailableBannerComponent Map(IDocumentSection input)
    {
        var mappable = FindMappableSection<GDSCookieChoiceAvailableBannerComponent>(input);
        return new()
        {
            Heading = _documentSectionFinder.FindMany(mappable, new CssElementSelector(".govuk-cookie-banner__heading"))
                .Single()
                .MapWith(_textMapper),

            CookieChoiceButtons = _documentSectionFinder.FindMany<GDSButtonComponent>(mappable).MapWith(_buttonMapper),

            ViewCookiesLink = _documentSectionFinder.FindMany<AnchorLinkComponent>(mappable)
                .Single()
                .MapWith(_linkMapper),
        };
    }

    protected override bool IsMappableFrom(IDocumentSection part) => part.GetAttribute("class")!.Contains("govuk-cookie-banner");
}
