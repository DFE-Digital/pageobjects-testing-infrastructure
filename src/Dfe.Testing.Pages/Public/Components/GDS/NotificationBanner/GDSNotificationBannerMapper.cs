using Dfe.Testing.Pages.Public.Components.Text;

namespace Dfe.Testing.Pages.Public.Components.GDS.NotificationBanner;
internal sealed class GDSNotificationBannerMapper : IComponentMapper<GDSNotificationBannerComponent>
{
    private readonly IMappingResultFactory _mappingResultFactory;
    private readonly IComponentMapper<TextComponent> _textMapper;
    private readonly IMapRequestFactory _mapRequestFactory;

    public GDSNotificationBannerMapper(
        IComponentMapper<TextComponent> textMapper,
        IMappingResultFactory mappingResultFactory,
        IMapRequestFactory mapRequestFactory)
    {
        ArgumentNullException.ThrowIfNull(textMapper);
        _textMapper = textMapper;
        _mappingResultFactory = mappingResultFactory;
        _mapRequestFactory = mapRequestFactory;
    }

    public MappedResponse<GDSNotificationBannerComponent> Map(IMapRequest<IDocumentSection> request)
    {
        MappedResponse<TextComponent> mappedHeading =
            _textMapper.Map(
                _mapRequestFactory.CreateRequestFrom(request, nameof(GDSNotificationBannerComponent.Heading)))
            .AddToMappingResults(request.MappedResults);

        MappedResponse<TextComponent> mappedContent =
            _textMapper.Map(
                _mapRequestFactory.CreateRequestFrom(request, nameof(GDSNotificationBannerComponent.Content)))
            .AddToMappingResults(request.MappedResults);

        GDSNotificationBannerComponent component = new()
        {
            Heading = mappedHeading.Mapped,
            Content = mappedContent.Mapped
        };

        return _mappingResultFactory.Create(
            component,
            MappingStatus.Success,
            request.Document);
    }
}
