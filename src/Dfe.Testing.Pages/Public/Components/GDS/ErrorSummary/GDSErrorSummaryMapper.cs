using Dfe.Testing.Pages.Public.Components.Core.Link;

namespace Dfe.Testing.Pages.Public.Components.GDS.ErrorSummary;
internal sealed class GDSErrorSummaryMapper : BaseDocumentSectionToComponentMapper<GDSErrorSummaryComponent>
{
    private readonly IMapper<IDocumentSection, AnchorLinkComponent> _anchorLinkMapper;

    public GDSErrorSummaryMapper(
        IDocumentSectionFinder documentSectionFinder,
        IMapper<IDocumentSection, AnchorLinkComponent> anchorLinkFactory) : base(documentSectionFinder)
    {
        ArgumentNullException.ThrowIfNull(anchorLinkFactory);
        ArgumentNullException.ThrowIfNull(documentSectionFinder);
        _anchorLinkMapper = anchorLinkFactory;
    }

    public override GDSErrorSummaryComponent Map(IDocumentSection input)
    {
        return new()
        {
            Heading = input.FindDescendant(new CssElementSelector(".govuk-error-summary__title"))?.Text ?? throw new ArgumentNullException("heading on error summary is null"),
            Errors = _documentSectionFinder.FindMany<AnchorLinkComponent>(input).Select(_anchorLinkMapper.Map)
        };
    }
    protected override bool IsMappableFrom(IDocumentSection part) => true; // TODO check contains errorsummary inside
}


