using Dfe.Testing.Pages.Public.Components.Link;
using Dfe.Testing.Pages.Public.Components.Text;

namespace Dfe.Testing.Pages.Public.Components.GDS.Footer;
internal sealed class GDSFooterMapper : IComponentMapper<GDSFooterComponent>
{
    private readonly IMapRequestFactory _mapRequestFactory;
    private readonly IMappingResultFactory _mappingResultFactory;
    private readonly IComponentMapper<TextComponent> _textComponentMapper;
    private readonly IComponentMapper<AnchorLinkComponent> _anchorLinkMapper;

    public GDSFooterMapper(
        IMapRequestFactory mapRequestFactory,
        IMappingResultFactory mappingResultFactory,
        IComponentMapper<TextComponent> textComponentMapper,
        IComponentMapper<AnchorLinkComponent> anchorLinKMapper)
    {
        ArgumentNullException.ThrowIfNull(anchorLinKMapper);
        _textComponentMapper = textComponentMapper;
        _anchorLinkMapper = anchorLinKMapper;
        _mapRequestFactory = mapRequestFactory;
        _mappingResultFactory = mappingResultFactory;
    }

    public MappedResponse<GDSFooterComponent> Map(IMapRequest<IDocumentSection> request)
    {
        MappedResponse<AnchorLinkComponent> mappedCrownCopyrightLink =
            _anchorLinkMapper.Map(
                _mapRequestFactory.CreateRequestFrom(request, nameof(GDSFooterComponent.CrownCopyrightLink)))
            .AddToMappingResults(request.MappedResults);

        MappedResponse<AnchorLinkComponent> mappedLicenseLink =
            _anchorLinkMapper.Map(
                _mapRequestFactory.CreateRequestFrom(request, nameof(GDSFooterComponent.LicenseLink)))
            .AddToMappingResults(request.MappedResults);

        MappedResponse<TextComponent> mappedLicenseMessage =
            _textComponentMapper.Map(
                _mapRequestFactory.CreateRequestFrom(request, nameof(GDSFooterComponent.LicenseMessage)))
            .AddToMappingResults(request.MappedResults);

        IEnumerable<MappedResponse<AnchorLinkComponent>> mappedApplicationLinks =
            _mapRequestFactory.CreateRequestFrom(request, nameof(GDSFooterComponent.ApplicationLinks))
                .FindManyDescendantsAndMapToComponent(_mapRequestFactory, _anchorLinkMapper)
                .AddToMappingResults(request.MappedResults);

        GDSFooterComponent component = new()
        {
            CrownCopyrightLink = mappedCrownCopyrightLink.Mapped,
            LicenseLink = mappedLicenseLink.Mapped,
            LicenseMessage = mappedLicenseMessage.Mapped,
            ApplicationLinks = mappedApplicationLinks.Select(t => t.Mapped),
        };

        return _mappingResultFactory.Create(
            component,
            MappingStatus.Success,
            request.Document);
    }
}
