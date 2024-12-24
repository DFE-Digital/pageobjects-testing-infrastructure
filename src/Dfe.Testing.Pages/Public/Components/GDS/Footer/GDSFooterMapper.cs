using Dfe.Testing.Pages.Public.Components.Core.Link;
using Dfe.Testing.Pages.Public.Components.Core.Text;

namespace Dfe.Testing.Pages.Public.Components.GDS.Footer;
internal sealed class GDSFooterMapper : BaseDocumentSectionToComponentMapper<GDSFooterComponent>
{
    private readonly IMapper<IDocumentSection, TextComponent> _textComponentMapper;
    private readonly IMapper<IDocumentSection, AnchorLinkComponent> _anchorLinkMapper;

    public GDSFooterMapper(
        IMapper<IDocumentSection, TextComponent> textComponentMapper,
        IMapper<IDocumentSection, AnchorLinkComponent> anchorLinKMapper,
        IDocumentSectionFinder documentSectionFinder) : base(documentSectionFinder)
    {
        ArgumentNullException.ThrowIfNull(anchorLinKMapper);
        _textComponentMapper = textComponentMapper;
        _anchorLinkMapper = anchorLinKMapper;
    }
    public override GDSFooterComponent Map(IDocumentSection input)
    {
        var mappable = FindMappableSection<GDSFooterComponent>(input);
        return new()
        {
            CrownCopyrightLink = _documentSectionFinder.Find(mappable, new CssElementSelector(".govuk-footer__link .govuk-footer__copyright-logo")).MapWith(_anchorLinkMapper),
            LicenseLink = _documentSectionFinder.Find(mappable, new CssElementSelector(".govuk-footer__licence-description .govuk-footer__link")).MapWith(_anchorLinkMapper),
            LicenseMessage = _documentSectionFinder.Find(mappable, new CssElementSelector(".govuk-footer__licence-description")).MapWith(_textComponentMapper),
            ApplicationLinks = _documentSectionFinder.FindMany(mappable, new CssElementSelector(".govuk-footer__link")).MapWith(_anchorLinkMapper)
        };
    }

    protected override bool IsMappableFrom(IDocumentSection section) => section.TagName.Equals("footer", StringComparison.OrdinalIgnoreCase);
}
