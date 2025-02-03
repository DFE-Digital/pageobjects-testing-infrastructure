using Dfe.Testing.Pages.Public.Components.Link;
using Dfe.Testing.Pages.Public.Components.Text;

namespace Dfe.Testing.Pages.Public.Components.GDS.Tabs;
internal sealed class GDSTabsMapper : IComponentMapper<GDSTabsComponent>
{
    private readonly IMapRequestFactory _mapRequestFactory;
    private readonly IComponentMapper<AnchorLinkComponent> _anchorLinkMapper;
    private readonly IComponentMapper<TextComponent> _textMapper;
    private readonly IMappingResultFactory _mappingResultFactory;

    public GDSTabsMapper(
        IMapRequestFactory mapRequestFactory,
        IComponentMapper<AnchorLinkComponent> anchorLinkFactory,
        IComponentMapper<TextComponent> textMapper,
        IMappingResultFactory mappingResultFactory)
    {
        ArgumentNullException.ThrowIfNull(anchorLinkFactory);
        ArgumentNullException.ThrowIfNull(textMapper);
        _mapRequestFactory = mapRequestFactory;
        _anchorLinkMapper = anchorLinkFactory;
        _textMapper = textMapper;
        _mappingResultFactory = mappingResultFactory;
    }

    public MappedResponse<GDSTabsComponent> Map(IMapRequest<IDocumentSection> request)
    {
        MappedResponse<TextComponent> headingResponse = _textMapper.Map(
            _mapRequestFactory.CreateRequestFrom(request, nameof(GDSTabsComponent.Heading)))
                .AddToMappingResults(request.MappedResults);

        IEnumerable<MappedResponse<AnchorLinkComponent>> links =
            _mapRequestFactory.CreateRequestFrom(request, nameof(GDSTabsComponent.Tabs))
                .FindManyDescendantsAndMapToComponent<AnchorLinkComponent>(_mapRequestFactory, _anchorLinkMapper);

        GDSTabsComponent tabs = new()
        {
            Heading = headingResponse.Mapped,
            Tabs = links.Select(t => t.Mapped!)
        };

        return _mappingResultFactory.Create(
            tabs,
            MappingStatus.Success,
            request.Document);
    }
}
