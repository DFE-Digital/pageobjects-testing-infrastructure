using Dfe.Testing.Pages.Public.Components.Link;
using Dfe.Testing.Pages.Public.Components.Text;

namespace Dfe.Testing.Pages.Public.Components.GDS.Footer;
internal sealed class GDSFooterMapper : IComponentMapper<GDSFooterComponent>
{
    private readonly IMapRequestFactory _mapRequestFactory;
    private readonly IMappingResultFactory _mappingResultFactory;
    private readonly IComponentMapper<TextComponent> _textComponentMapper;
    private readonly IComponentMapper<AnchorLinkComponentOld> _anchorLinkMapper;

    public GDSFooterMapper(
        IMapRequestFactory mapRequestFactory,
        IMappingResultFactory mappingResultFactory,
        IComponentMapper<TextComponent> textComponentMapper,
        IComponentMapper<AnchorLinkComponentOld> anchorLinKMapper)
    {
        ArgumentNullException.ThrowIfNull(anchorLinKMapper);
        _textComponentMapper = textComponentMapper;
        _anchorLinkMapper = anchorLinKMapper;
        _mapRequestFactory = mapRequestFactory;
        _mappingResultFactory = mappingResultFactory;
    }

    public MappedResponse<GDSFooterComponent> Map(IMapRequest<IDocumentSection> request)
    {
        MappedResponse<AnchorLinkComponentOld> mappedCrownCopyrightLink =
            _anchorLinkMapper.Map(
                _mapRequestFactory.CreateRequestFrom(request, nameof(GDSFooterComponent.CrownCopyrightLink)));

        MappedResponse<AnchorLinkComponentOld> mappedLicenseLink =
            _anchorLinkMapper.Map(
                _mapRequestFactory.CreateRequestFrom(request, nameof(GDSFooterComponent.LicenseLink)));

        MappedResponse<TextComponent> mappedLicenseMessage =
            _textComponentMapper.Map(
                _mapRequestFactory.CreateRequestFrom(request, nameof(GDSFooterComponent.LicenseMessage)));

        IEnumerable<MappedResponse<AnchorLinkComponentOld>> mappedApplicationLinks =
            _mapRequestFactory.CreateRequestFrom(request, nameof(GDSFooterComponent.ApplicationLinks))
                .FindManyDescendantsAndMapToComponent(_mapRequestFactory, _anchorLinkMapper);

        GDSFooterComponent component = new()
        {
            CrownCopyrightLink = mappedCrownCopyrightLink.Mapped,
            LicenseLink = mappedLicenseLink.Mapped,
            LicenseMessage = mappedLicenseMessage.Mapped,
            ApplicationLinks = mappedApplicationLinks.Select(t => t.Mapped),
        };

        return _mappingResultFactory.Create(
            request.Options.MapKey,
            component,
            MappingStatus.Success,
            request.Document)
                .AddToMappingResults(mappedCrownCopyrightLink.MappingResults)
                .AddToMappingResults(mappedLicenseLink.MappingResults)
                .AddToMappingResults(mappedLicenseMessage.MappingResults)
                .AddToMappingResults(mappedApplicationLinks.SelectMany(t => t.MappingResults));
    }
}
