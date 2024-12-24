using Dfe.Testing.Pages.Public.Components.Core.Link;
using Dfe.Testing.Pages.Public.Components.Core.Text;

namespace Dfe.Testing.Pages.Public.Components.GDS.Tabs;
internal sealed class GDSTabsMapper : BaseDocumentSectionToComponentMapper<GDSTabsComponent>
{
    private readonly IMapper<IDocumentSection, AnchorLinkComponent> _anchorLinkFactory;
    private readonly IMapper<IDocumentSection, TextComponent> _textMapper;

    public GDSTabsMapper(
        IDocumentSectionFinder documentSectionFinder,
        IMapper<IDocumentSection, AnchorLinkComponent> anchorLinkFactory,
        IMapper<IDocumentSection, TextComponent> textMapper) : base(documentSectionFinder)
    {
        ArgumentNullException.ThrowIfNull(anchorLinkFactory);
        ArgumentNullException.ThrowIfNull(textMapper);
        _anchorLinkFactory = anchorLinkFactory;
        _textMapper = textMapper;
    }
    public override GDSTabsComponent Map(IDocumentSection section)
    {
        var mappable = FindMappableSection<GDSTabsComponent>(section);
        return new GDSTabsComponent()
        {
            Heading = _documentSectionFinder.Find(mappable, new CssElementSelector(".govuk-tabs__title")).MapWith(_textMapper),
            Tabs = _documentSectionFinder.FindMany(mappable, new CssElementSelector(".govuk-tabs__list")).Select(_anchorLinkFactory.Map)
        };
    }

    protected override bool IsMappableFrom(IDocumentSection section) => true; // TODO check contains tabs inside
}
