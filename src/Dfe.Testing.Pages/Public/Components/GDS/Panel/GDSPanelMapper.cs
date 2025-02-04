using Dfe.Testing.Pages.Public.Components.Text;

namespace Dfe.Testing.Pages.Public.Components.GDS.Panel;
internal sealed class GDSPanelMapper : IComponentMapper<GDSPanelComponent>
{
    private readonly IMappingResultFactory _mappingResultFactory;
    private readonly IMapRequestFactory _mapRequestFactory;
    private readonly IComponentMapper<TextComponent> _textMapper;

    public GDSPanelMapper(
        IMapRequestFactory mapRequestFactory,
        IComponentMapper<TextComponent> textMapper,
        IMappingResultFactory mappingResultFactory)
    {
        ArgumentNullException.ThrowIfNull(textMapper);
        _mapRequestFactory = mapRequestFactory;
        _textMapper = textMapper;
        _mappingResultFactory = mappingResultFactory;
    }

    public MappedResponse<GDSPanelComponent> Map(IMapRequest<IDocumentSection> request)
    {
        // Heading
        MappedResponse<TextComponent> mappedHeading =
            _textMapper.Map(
                _mapRequestFactory.CreateRequestFrom(request, nameof(GDSPanelComponent.Heading)))
            .AddToMappingResults(request.MappedResults);

        // Content
        MappedResponse<TextComponent> mappedContent =
            _textMapper.Map(
                _mapRequestFactory.CreateRequestFrom(request, nameof(GDSPanelComponent.Content)))
            .AddToMappingResults(request.MappedResults);

        return _mappingResultFactory.Create(
            request.Options.MapKey,
            new GDSPanelComponent()
            {
                Heading = mappedHeading.Mapped,
                Content = mappedContent.Mapped,
            },
            MappingStatus.Success,
            request.Document);
    }
}
