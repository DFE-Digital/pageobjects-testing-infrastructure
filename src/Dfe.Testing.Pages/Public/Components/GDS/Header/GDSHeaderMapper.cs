using Dfe.Testing.Pages.Public.Components.Link;

namespace Dfe.Testing.Pages.Public.Components.GDS.Header;
internal sealed class GDSHeaderMapper : IComponentMapper<GDSHeaderComponent>
{
    private readonly IMapRequestFactory _mapRequestFactory;
    private readonly IMappingResultFactory _mappingResultFactory;
    private readonly IComponentMapper<AnchorLinkComponentOld> _linkMapper;

    public GDSHeaderMapper(
        IMapRequestFactory mapRequestFactory,
        IMappingResultFactory mappingResultFactory,
        IComponentMapper<AnchorLinkComponentOld> linkMapper)
    {
        ArgumentNullException.ThrowIfNull(linkMapper);
        _mapRequestFactory = mapRequestFactory;
        _mappingResultFactory = mappingResultFactory;
        _linkMapper = linkMapper;
    }

    public MappedResponse<GDSHeaderComponent> Map(IMapRequest<IDocumentSection> request)
    {
        MappedResponse<AnchorLinkComponentOld> mappedGovUKLink =
            _linkMapper.Map(
                _mapRequestFactory.CreateRequestFrom(request, nameof(GDSHeaderComponent.GovUKLink)));

        IEnumerable<MappedResponse<AnchorLinkComponentOld>> mappedNavigationLinks =
            _mapRequestFactory.CreateRequestFrom(request, nameof(GDSHeaderComponent.NavigationLinks))
                .FindManyDescendantsAndMapToComponent<AnchorLinkComponentOld>(_mapRequestFactory, _linkMapper);

        GDSHeaderComponent component = new()
        {
            GovUKLink = mappedGovUKLink.Mapped,
            NavigationLinks = mappedNavigationLinks.Select(t => t.Mapped)
        };

        return _mappingResultFactory.Create(
            request.Options.MapKey,
            component,
            MappingStatus.Success,
            request.Document)
                .AddToMappingResults(mappedGovUKLink.MappingResults)
                .AddToMappingResults(mappedNavigationLinks.SelectMany(t => t.MappingResults));
    }
}
