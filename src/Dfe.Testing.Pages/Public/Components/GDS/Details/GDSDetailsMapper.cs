using Dfe.Testing.Pages.Public.Components.Text;

namespace Dfe.Testing.Pages.Public.Components.GDS.Details;
internal sealed class GDSDetailsMapper : IComponentMapper<GDSDetailsComponent>
{
    private readonly IMapRequestFactory _mapRequestFactory;
    private readonly IMappingResultFactory _mappingResultFactory;
    private readonly IComponentMapper<TextComponent> _textMapper;

    public GDSDetailsMapper(
        IMapRequestFactory mapRequestFactory,
        IMappingResultFactory mappingResultFactory,
        IComponentMapper<TextComponent> textMapper)
    {
        _mapRequestFactory = mapRequestFactory;
        _mappingResultFactory = mappingResultFactory;
        _textMapper = textMapper;
    }

    public MappedResponse<GDSDetailsComponent> Map(IMapRequest<IDocumentSection> request)
    {
        MappedResponse<TextComponent> summary =
            _textMapper.Map(
                _mapRequestFactory.CreateRequestFrom(request, nameof(GDSDetailsComponent.Summary)))
            .AddToMappingResults(request.MappedResults);

        MappedResponse<TextComponent> content =
            _textMapper.Map(
                _mapRequestFactory.CreateRequestFrom(request, nameof(GDSDetailsComponent.Content)))
            .AddToMappingResults(request.MappedResults);

        GDSDetailsComponent component = new()
        {
            Summary = summary.Mapped,
            Content = content.Mapped
        };

        return _mappingResultFactory.Create(
            component,
            MappingStatus.Success,
            request.Document);
    }
}
