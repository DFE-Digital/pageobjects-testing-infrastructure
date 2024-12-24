using Dfe.Testing.Pages.Public.Components.Core.Text;

namespace Dfe.Testing.Pages.Public.Components.GDS.Panel;
internal sealed class GDSPanelMapper : BaseDocumentSectionToComponentMapper<GDSPanelComponent>
{
    private readonly IMapper<IDocumentSection, TextComponent> _textMapper;

    public GDSPanelMapper(
        IDocumentSectionFinder documentSectionFinder,
        IMapper<IDocumentSection, TextComponent> textMapper) : base(documentSectionFinder)
    {
        ArgumentNullException.ThrowIfNull(textMapper);
        _textMapper = textMapper;
    }

    public override GDSPanelComponent Map(IDocumentSection input)
    {
        var mappable = FindMappableSection<GDSPanelComponent>(input);
        return new()
        {
            Heading = _documentSectionFinder.Find(mappable, new CssElementSelector(".govuk-panel__title")).MapWith(_textMapper),
            Content = _documentSectionFinder.Find(mappable, new CssElementSelector(".govuk-panel__body")).MapWith(_textMapper),
        };
    }

    protected override bool IsMappableFrom(IDocumentSection section) => true; // TODO panel
}
