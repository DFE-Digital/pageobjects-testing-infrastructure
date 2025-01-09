using Dfe.Testing.Pages.Public.Components.MappingAbstraction.Request;
using Dfe.Testing.Pages.Public.Components.Text;

namespace Dfe.Testing.Pages.Public.Components.GDS.Panel;
internal sealed class GDSPanelMapper : IMapper<IMapRequest<IDocumentSection>, MappedResponse<GDSPanelComponent>>
{
    private readonly IMapRequestFactory _mapRequestFactory;
    private readonly IMappingResultFactory _mappingResultFactory;
    private readonly IMapper<IMapRequest<IDocumentSection>, MappedResponse<TextComponent>> _textMapper;

    public GDSPanelMapper(
        IMapRequestFactory mapRequestFactory,
        IMapper<IMapRequest<IDocumentSection>, MappedResponse<TextComponent>> textMapper,
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
        MappedResponse<TextComponent> mappedHeading = _textMapper.Map(
            _mapRequestFactory.Create(
                request.From,
                request.MappingResults,
                new CssElementSelector(".govuk-panel__title")))
            .AddMappedResponseToResults(request.MappingResults);

        // Content
        MappedResponse<TextComponent> mappedContent = _textMapper.Map(
            _mapRequestFactory.Create(
                request.From,
                request.MappingResults,
                new CssElementSelector(".govuk-panel__body")))
            .AddMappedResponseToResults(request.MappingResults);

        GDSPanelComponent panel = new()
        {
            Heading = mappedHeading.Mapped,
            Content = mappedContent.Mapped
        };

        return _mappingResultFactory.Create(
            panel,
            MappingStatus.Success,
            request.From);
    }
}
