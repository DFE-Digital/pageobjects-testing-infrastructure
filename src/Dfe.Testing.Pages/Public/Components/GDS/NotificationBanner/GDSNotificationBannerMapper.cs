using Dfe.Testing.Pages.Public.Components.MappingAbstraction.Request;
using Dfe.Testing.Pages.Public.Components.Text;

namespace Dfe.Testing.Pages.Public.Components.GDS.NotificationBanner;
internal sealed class GDSNotificationBannerMapper : IMapper<IMapRequest<IDocumentSection>, MappedResponse<GDSNotificationBannerComponent>>
{
    private readonly IMappingResultFactory _mappingResultFactory;
    private readonly IMapRequestFactory _mapRequestFactory;
    private readonly IMapper<IMapRequest<IDocumentSection>, MappedResponse<TextComponent>> _textMapper;

    public GDSNotificationBannerMapper(
        IMapRequestFactory mapRequestFactory,
        IMapper<IMapRequest<IDocumentSection>, MappedResponse<TextComponent>> textMapper,
        IMappingResultFactory mappingResultFactory)
    {
        ArgumentNullException.ThrowIfNull(textMapper);
        _mapRequestFactory = mapRequestFactory;
        _textMapper = textMapper;
        _mappingResultFactory = mappingResultFactory;
    }

    public MappedResponse<GDSNotificationBannerComponent> Map(IMapRequest<IDocumentSection> request)
    {
        MappedResponse<TextComponent> mappedHeading = _textMapper.Map(
            _mapRequestFactory.Create(
                request.From,
                request.MappingResults,
                new CssElementSelector(".govuk-notification-banner__title")))
            .AddMappedResponseToResults(request.MappingResults);

        MappedResponse<TextComponent> mappedContent = _textMapper.Map(
            _mapRequestFactory.Create(
                request.From,
                request.MappingResults,
                new CssElementSelector(".govuk-notification-banner__content")))
        .AddMappedResponseToResults(request.MappingResults);

        GDSNotificationBannerComponent component = new()
        {
            Heading = mappedHeading.Mapped,
            Content = mappedContent.Mapped
        };

        return _mappingResultFactory.Create(
            component,
            MappingStatus.Success,
            request.From);
    }
}
