using Dfe.Testing.Pages.Public.Components.Core.Link;
using Dfe.Testing.Pages.Public.Components.Core.Text;
using Dfe.Testing.Pages.Public.Components.GDS.Button;

namespace Dfe.Testing.Pages.Public.Components.GDS.CookieBanner;
internal sealed class GDSCookieChoiceMadeBannerMappper : BaseDocumentSectionToComponentMapper<GDSCookieChoiceMadeBannerComponent>
{
    private readonly IMapper<IDocumentSection, TextComponent> _textMapper;
    private readonly IMapper<IDocumentSection, AnchorLinkComponent> _linkMapper;
    private readonly IMapper<IDocumentSection, GDSButtonComponent> _buttonMapper;

    public GDSCookieChoiceMadeBannerMappper(
        IDocumentSectionFinder documentSectionFinder,
        IMapper<IDocumentSection, TextComponent> textFactory,
        IMapper<IDocumentSection, AnchorLinkComponent> linkFactory,
        IMapper<IDocumentSection, GDSButtonComponent> buttonFactory) : base(documentSectionFinder)
    {
        _textMapper = textFactory;
        _linkMapper = linkFactory;
        _buttonMapper = buttonFactory;
    }

    public override GDSCookieChoiceMadeBannerComponent Map(IDocumentSection input)
    {
        var mappable = FindMappableSection<GDSCookieChoiceMadeBannerComponent>(input);
        return new()
        {
            Message = _documentSectionFinder.FindMany(mappable, new CssElementSelector(".govuk-cookie-banner_content"))
                    .Single()
                    .MapWith(_textMapper),

            ChangeYourCookieSettingsLink = _documentSectionFinder.FindMany<AnchorLinkComponent>(mappable)
                    .Single()
                    .MapWith(_linkMapper),

            HideCookies = _documentSectionFinder.FindMany<GDSButtonComponent>(mappable)
                    .Single()
                    .MapWith(_buttonMapper)
        };
    }

    protected override bool IsMappableFrom(IDocumentSection part) => part.GetAttribute("class")!.Contains("govuk-cookie-banner");
}
