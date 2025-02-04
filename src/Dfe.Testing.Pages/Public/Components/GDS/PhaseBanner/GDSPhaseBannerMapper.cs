using Dfe.Testing.Pages.Public.Components.Link;
using Dfe.Testing.Pages.Public.Components.Text;

namespace Dfe.Testing.Pages.Public.Components.GDS.PhaseBanner;
internal sealed class GDSPhaseBannerMapper : IComponentMapper<GDSPhaseBannerComponent>
{
    private readonly IMapRequestFactory _mapRequestFactory;
    private readonly IMappingResultFactory _mapResultFactory;
    private readonly IComponentMapper<TextComponent> _textMapper;
    private readonly IComponentMapper<AnchorLinkComponent> _linkMapper;

    public GDSPhaseBannerMapper(
        IMappingResultFactory mapResultFactory,
        IComponentMapper<TextComponent> textMapper,
        IComponentMapper<AnchorLinkComponent> linkMapper,
        IMapRequestFactory mapRequestFactory)
    {
        _mapResultFactory = mapResultFactory;
        _textMapper = textMapper;
        _linkMapper = linkMapper;
        _mapRequestFactory = mapRequestFactory;
    }

    public MappedResponse<GDSPhaseBannerComponent> Map(IMapRequest<IDocumentSection> request)
    {
        MappedResponse<TextComponent> mappedPhase = _textMapper.Map(
            _mapRequestFactory.CreateRequestFrom(request, nameof(GDSPhaseBannerComponent.Phase)))
                .AddToMappingResults(request.MappedResults);

        MappedResponse<TextComponent> mappedText = _textMapper.Map(
            _mapRequestFactory.CreateRequestFrom(request, nameof(GDSPhaseBannerComponent.Text)))
                .AddToMappingResults(request.MappedResults);

        MappedResponse<AnchorLinkComponent> mappedFeedbackLink = _linkMapper.Map(
            _mapRequestFactory.CreateRequestFrom(request, nameof(GDSPhaseBannerComponent.FeedbackLink)))
                .AddToMappingResults(request.MappedResults);

        return _mapResultFactory.Create(
            request.Options.MapKey,
            new GDSPhaseBannerComponent
            {
                Phase = mappedPhase.Mapped!,
                Text = mappedText.Mapped!,
                FeedbackLink = mappedFeedbackLink.Mapped!
            },
            MappingStatus.Success,
            request.Document);
    }
}
