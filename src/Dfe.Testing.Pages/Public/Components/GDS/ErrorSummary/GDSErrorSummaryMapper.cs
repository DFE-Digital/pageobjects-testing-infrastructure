using Dfe.Testing.Pages.Public.Components.Link;
using Dfe.Testing.Pages.Public.Components.Text;

namespace Dfe.Testing.Pages.Public.Components.GDS.ErrorSummary;
internal sealed class GDSErrorSummaryMapper : IComponentMapper<GDSErrorSummaryComponent>
{
    private readonly IMapRequestFactory _mapRequestFactory;
    private readonly IMappingResultFactory _mappingResultFactory;
    private readonly IComponentMapper<TextComponent> _textMapper;
    private readonly IComponentMapper<AnchorLinkComponent> _anchorLinkMapper;

    public GDSErrorSummaryMapper(
        IMappingResultFactory mappingResultFactory,
        IComponentMapper<TextComponent> textMapper,
        IComponentMapper<AnchorLinkComponent> anchorLinkMapper,
        IMapRequestFactory mapRequestFactory)
    {
        _mappingResultFactory = mappingResultFactory;
        _textMapper = textMapper;
        _anchorLinkMapper = anchorLinkMapper;
        _mapRequestFactory = mapRequestFactory;
    }

    public MappedResponse<GDSErrorSummaryComponent> Map(IMapRequest<IDocumentSection> request)
    {
        MappedResponse<TextComponent> mappedHeading =
            _textMapper.Map(
                _mapRequestFactory.CreateRequestFrom(request, nameof(GDSErrorSummaryComponent.Heading)))
                .AddToMappingResults(request.MappedResults);

        IEnumerable<MappedResponse<AnchorLinkComponent>> mappedErrors =
            _mapRequestFactory.CreateRequestFrom(request, nameof(GDSErrorSummaryComponent.Errors))
                .FindManyDescendantsAndMapToComponent(_mapRequestFactory, _anchorLinkMapper)
                .AddToMappingResults(request.MappedResults);

        GDSErrorSummaryComponent component = new()
        {
            Heading = mappedHeading.Mapped,
            Errors = mappedErrors.Select(t => t.Mapped)
        };

        return _mappingResultFactory.Create(
            component,
            MappingStatus.Success,
            request.Document);
    }
}
