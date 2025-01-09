using Dfe.Testing.Pages.Public.Components.Link;
using Dfe.Testing.Pages.Public.Components.MappingAbstraction.Request;
using Dfe.Testing.Pages.Public.Components.SelectorFactory;
using Dfe.Testing.Pages.Public.Components.Text;

namespace Dfe.Testing.Pages.Public.Components.GDS.Tabs;
internal sealed class GDSTabsMapper : IMapper<IMapRequest<IDocumentSection>, MappedResponse<GDSTabsComponent>>
{
    private readonly IMapRequestFactory _mapRequestFactory;
    private readonly IComponentSelectorFactory _componentSelectorFactory;
    private readonly IMapper<IMapRequest<IDocumentSection>, MappedResponse<AnchorLinkComponent>> _anchorLinkMapper;
    private readonly IMapper<IMapRequest<IDocumentSection>, MappedResponse<TextComponent>> _textMapper;
    private readonly IMappingResultFactory _mappingResultFactory;

    public GDSTabsMapper(
        IMapRequestFactory mapRequestFactory,
        IComponentSelectorFactory componentSelectorFactory,
        IMapper<IMapRequest<IDocumentSection>, MappedResponse<AnchorLinkComponent>> anchorLinkFactory,
        IMapper<IMapRequest<IDocumentSection>, MappedResponse<TextComponent>> textMapper,
        IMappingResultFactory mappingResultFactory)
    {
        ArgumentNullException.ThrowIfNull(anchorLinkFactory);
        ArgumentNullException.ThrowIfNull(textMapper);
        _mapRequestFactory = mapRequestFactory;
        _componentSelectorFactory = componentSelectorFactory;
        _anchorLinkMapper = anchorLinkFactory;
        _textMapper = textMapper;
        _mappingResultFactory = mappingResultFactory;
    }
    public MappedResponse<GDSTabsComponent> Map(IMapRequest<IDocumentSection> request)
    {
        MappedResponse<TextComponent> headingResponse = _textMapper.Map(
            _mapRequestFactory.Create(
                request.From,
                request.MappingResults,
                new CssElementSelector(".govuk-tabs__title")))
        .AddMappedResponseToResults(request.MappingResults);

        IEnumerable<MappedResponse<AnchorLinkComponent>> links =
            request.FindManyDescendantsAndMap<AnchorLinkComponent>(
                _mapRequestFactory,
                new CssElementSelector(".govuk-tabs__list"),
                _anchorLinkMapper);

        GDSTabsComponent tabs = new()
        {
            Heading = headingResponse.Mapped,
            Tabs = links.Select(t => t.Mapped!)
        };

        return _mappingResultFactory.Create(
            tabs,
            MappingStatus.Success,
            request.From);
    }
}
