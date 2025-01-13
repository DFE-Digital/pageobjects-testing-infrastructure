using Dfe.Testing.Pages.Public.Components.Link;
using Dfe.Testing.Pages.Public.Components.MappingAbstraction.Request;
using Dfe.Testing.Pages.Public.Components.SelectorFactory;

namespace Dfe.Testing.Pages.Public.Components.GDS.Header;
internal sealed class GDSHeaderMapper : IMapper<IMapRequest<IDocumentSection>, MappedResponse<GDSHeaderComponent>>
{
    private readonly IMapRequestFactory _mapRequestFactory;
    private readonly IMappingResultFactory _mappingResultFactory;
    private readonly IComponentSelectorFactory _componentSelectorFactory;
    private readonly IMapper<IMapRequest<IDocumentSection>, MappedResponse<AnchorLinkComponent>> _linkMapper;

    public GDSHeaderMapper(
        IMapRequestFactory mapRequestFactory,
        IMappingResultFactory mappingResultFactory,
        IComponentSelectorFactory componentSelectorFactory,
        IMapper<IMapRequest<IDocumentSection>, MappedResponse<AnchorLinkComponent>> linkMapper)
    {
        ArgumentNullException.ThrowIfNull(linkMapper);
        _mapRequestFactory = mapRequestFactory;
        _mappingResultFactory = mappingResultFactory;
        _componentSelectorFactory = componentSelectorFactory;
        _linkMapper = linkMapper;
    }
    public MappedResponse<GDSHeaderComponent> Map(IMapRequest<IDocumentSection> request)
    {
        MappedResponse<AnchorLinkComponent> mappedGovUKLink = _linkMapper.Map(
            _mapRequestFactory.Create(
                request.From,
                request.MappingResults,
                new CssElementSelector(".govuk-header__link--homepage")))
            .AddMappedResponseToResults(request.MappingResults);

        IEnumerable<MappedResponse<AnchorLinkComponent>> mappedNavigationLinks = request.FindManyDescendantsAndMap<AnchorLinkComponent>(
            _mapRequestFactory,
            new CssElementSelector(".govuk-header nav a"),
            _linkMapper)
        .AddMappedResponseToResults(request.MappingResults);

        GDSHeaderComponent component = new()
        {
            GovUKLink = mappedGovUKLink.Mapped,
            NavigationLinks = mappedNavigationLinks.Select(t => t.Mapped)
        };

        return _mappingResultFactory.Create(
            component,
            MappingStatus.Success,
            request.From);
    }
}
