﻿using Dfe.Testing.Pages.Public.Components.Link;

namespace Dfe.Testing.Pages.Public.Components.GDS.Header;
internal sealed class GDSHeaderMapper : IComponentMapper<GDSHeaderComponent>
{
    private readonly IMapRequestFactory _mapRequestFactory;
    private readonly IMappingResultFactory _mappingResultFactory;
    private readonly IComponentMapper<AnchorLinkComponent> _linkMapper;

    public GDSHeaderMapper(
        IMapRequestFactory mapRequestFactory,
        IMappingResultFactory mappingResultFactory,
        IComponentMapper<AnchorLinkComponent> linkMapper)
    {
        ArgumentNullException.ThrowIfNull(linkMapper);
        _mapRequestFactory = mapRequestFactory;
        _mappingResultFactory = mappingResultFactory;
        _linkMapper = linkMapper;
    }

    public MappedResponse<GDSHeaderComponent> Map(IMapRequest<IDocumentSection> request)
    {
        MappedResponse<AnchorLinkComponent> mappedGovUKLink =
            _linkMapper.Map(
                _mapRequestFactory.CreateRequestFrom(request, nameof(GDSHeaderComponent.GovUKLink)))
            .AddToMappingResults(request.MappedResults);

        IEnumerable<MappedResponse<AnchorLinkComponent>> mappedNavigationLinks =
            _mapRequestFactory.CreateRequestFrom(request, nameof(GDSHeaderComponent.NavigationLinks))
                .FindManyDescendantsAndMapToComponent<AnchorLinkComponent>(_mapRequestFactory, _linkMapper)
                .AddToMappingResults(request.MappedResults);

        GDSHeaderComponent component = new()
        {
            GovUKLink = mappedGovUKLink.Mapped,
            NavigationLinks = mappedNavigationLinks.Select(t => t.Mapped)
        };

        return _mappingResultFactory.Create(
            component,
            MappingStatus.Success,
            request.Document);
    }
}
