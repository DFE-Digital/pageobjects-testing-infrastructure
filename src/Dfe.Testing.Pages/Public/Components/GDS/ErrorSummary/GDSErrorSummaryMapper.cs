using Dfe.Testing.Pages.Public.Components.Link;
using Dfe.Testing.Pages.Public.Components.MappingAbstraction.Request;
using Dfe.Testing.Pages.Public.Components.SelectorFactory;
using Dfe.Testing.Pages.Public.Components.Text;

namespace Dfe.Testing.Pages.Public.Components.GDS.ErrorSummary;
internal sealed class GDSErrorSummaryMapper : IMapper<IMapRequest<IDocumentSection>, MappedResponse<GDSErrorSummaryComponent>>
{
    private readonly IMapRequestFactory _mapRequestFactory;
    private readonly IMappingResultFactory _mappingResultFactory;
    private readonly IComponentSelectorFactory _componentSelectorFactory;
    private readonly IMapper<IMapRequest<IDocumentSection>, MappedResponse<TextComponent>> _textMapper;
    private readonly IMapper<IMapRequest<IDocumentSection>, MappedResponse<AnchorLinkComponent>> _anchorLinkMapper;

    public GDSErrorSummaryMapper(
        IMapRequestFactory mapRequestFactory,
        IMappingResultFactory mappingResultFactory,
        IComponentSelectorFactory componentSelectorFactory,
        IMapper<IMapRequest<IDocumentSection>, MappedResponse<TextComponent>> textMapper,
        IMapper<IMapRequest<IDocumentSection>, MappedResponse<AnchorLinkComponent>> anchorLinkMapper)
    {
        ArgumentNullException.ThrowIfNull(anchorLinkMapper);
        ArgumentNullException.ThrowIfNull(componentSelectorFactory);
        _mappingResultFactory = mappingResultFactory;
        _componentSelectorFactory = componentSelectorFactory;
        _textMapper = textMapper;
        _anchorLinkMapper = anchorLinkMapper;
        _mapRequestFactory = mapRequestFactory;
    }

    public MappedResponse<GDSErrorSummaryComponent> Map(IMapRequest<IDocumentSection> request)
    {
        // Heading
        MappedResponse<TextComponent> mappedHeading = _textMapper.Map(
            _mapRequestFactory.Create(
                request.From,
                request.MappingResults,
                new CssElementSelector(".govuk-error-summary__title")));
        request.MappingResults.Add(mappedHeading.MappingResult);

        // Errors
        IEnumerable<MappedResponse<AnchorLinkComponent>> mappedErrors = request.FindManyDescendantsAndMap(
            _mapRequestFactory,
            _componentSelectorFactory.GetSelector<AnchorLinkComponent>(),
            _anchorLinkMapper)
        .AddMappedResponseToResults(request.MappingResults);

        GDSErrorSummaryComponent component = new()
        {
            Heading = mappedHeading.Mapped,
            Errors = mappedErrors.Select(t => t.Mapped)
        };

        return _mappingResultFactory.Create(
            component,
            MappingStatus.Success,
            request.From);
    }
}
