using Dfe.Testing.Pages.Public.Components.Core.Link;

namespace Dfe.Testing.Pages.Public.Components.GDS.Header;
internal sealed class GDSHeaderMapper : BaseDocumentSectionToComponentMapper<GDSHeaderComponent>
{
    private readonly IMapper<IDocumentSection, AnchorLinkComponent> _linkMapper;

    public GDSHeaderMapper(
        IDocumentSectionFinder documentSectionFinder,
        IMapper<IDocumentSection, AnchorLinkComponent> linkMapper) : base(documentSectionFinder)
    {
        ArgumentNullException.ThrowIfNull(linkMapper);
        _linkMapper = linkMapper;
    }
    public override GDSHeaderComponent Map(IDocumentSection input)
    {
        var mappable = FindMappableSection<GDSHeaderComponent>(input);
        return new GDSHeaderComponent()
        {
            GovUKLink = _documentSectionFinder.Find(mappable, new CssElementSelector(".govuk-header__link--homepage")).MapWith(_linkMapper),
            NavigationLinks = _documentSectionFinder.FindMany(mappable, new CssElementSelector(".govuk-header nav")).Select(_linkMapper.Map),
        };
    }

    protected override bool IsMappableFrom(IDocumentSection section) => section.TagName.Equals("header", StringComparison.OrdinalIgnoreCase);
}
