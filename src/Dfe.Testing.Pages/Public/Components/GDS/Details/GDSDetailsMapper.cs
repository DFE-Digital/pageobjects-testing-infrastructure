using Dfe.Testing.Pages.Public.Components.Core.Text;

namespace Dfe.Testing.Pages.Public.Components.GDS.Details;
internal sealed class GDSDetailsMapper : BaseDocumentSectionToComponentMapper<GDSDetailsComponent>
{
    private readonly IMapper<IDocumentSection, TextComponent> _textMapper;

    public GDSDetailsMapper(
        IDocumentSectionFinder documentSectionFinder,
        IMapper<IDocumentSection, TextComponent> textMapper) : base(documentSectionFinder)
    {
        _textMapper = textMapper;
    }

    public override GDSDetailsComponent Map(IDocumentSection input)
    {
        var mappable = FindMappableSection<GDSDetailsComponent>(input);
        return new()
        {
            Summary = _documentSectionFinder.FindMany(mappable, new CssElementSelector(".govuk-details__summary"))
                .Single()
                .MapWith(_textMapper),

            Content = _documentSectionFinder.FindMany(mappable, new CssElementSelector(".govuk-details__text"))
                .Single()
                .MapWith(_textMapper)
        };
    }

    protected override bool IsMappableFrom(IDocumentSection part) => part.TagName.Contains("details", StringComparison.OrdinalIgnoreCase);
}
