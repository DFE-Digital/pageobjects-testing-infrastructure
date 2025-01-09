using Dfe.Testing.Pages.Public.Components.Link;
using Dfe.Testing.Pages.Public.Components.MappingAbstraction.Request;
using Dfe.Testing.Pages.Public.Components.SelectorFactory;
using Dfe.Testing.Pages.Public.Components.Text;

namespace Dfe.Testing.Pages.Public.Components.GDS.Footer;
internal sealed class GDSFooterMapper : IMapper<IMapRequest<IDocumentSection>, MappedResponse<GDSFooterComponent>>
{
    private readonly IMapRequestFactory _mapRequestFactory;
    private readonly IMappingResultFactory _mappingResultFactory;
    private readonly IMapper<IMapRequest<IDocumentSection>, MappedResponse<TextComponent>> _textComponentMapper;
    private readonly IMapper<IMapRequest<IDocumentSection>, MappedResponse<AnchorLinkComponent>> _anchorLinkMapper;
    private readonly IComponentSelectorFactory _componentSelectorFactory;

    public GDSFooterMapper(
        IMapRequestFactory mapRequestFactory,
        IMappingResultFactory mappingResultFactory,
        IComponentSelectorFactory componentSelectorFactory,
        IMapper<IMapRequest<IDocumentSection>, MappedResponse<TextComponent>> textComponentMapper,
        IMapper<IMapRequest<IDocumentSection>, MappedResponse<AnchorLinkComponent>> anchorLinKMapper)
    {
        ArgumentNullException.ThrowIfNull(anchorLinKMapper);
        _textComponentMapper = textComponentMapper;
        _anchorLinkMapper = anchorLinKMapper;
        _componentSelectorFactory = componentSelectorFactory;
        _mapRequestFactory = mapRequestFactory;
        _mappingResultFactory = mappingResultFactory;
    }

    public MappedResponse<GDSFooterComponent> Map(IMapRequest<IDocumentSection> request)
    {
        MappedResponse<AnchorLinkComponent> mappedCrownCopyrightLink = _anchorLinkMapper.Map(
            _mapRequestFactory.Create(
                request.From,
                request.MappingResults,
                new CssElementSelector(".govuk-footer__link .govuk-footer__copyright-logo")));
        request.MappingResults.Add(mappedCrownCopyrightLink.MappingResult);

        MappedResponse<AnchorLinkComponent> mappedLicenseLink = _anchorLinkMapper.Map(
            _mapRequestFactory.Create(
                request.From,
                request.MappingResults,
                new CssElementSelector(".govuk-footer__licence-description .govuk-footer__link")));
        request.MappingResults.Add(mappedLicenseLink.MappingResult);

        MappedResponse<TextComponent> mappedLicenseMessage = _textComponentMapper.Map(
            _mapRequestFactory.Create(
                request.From,
                request.MappingResults,
                new CssElementSelector(".govuk-footer__licence-description")));
        request.MappingResults.Add(mappedLicenseMessage.MappingResult);

        IEnumerable<MappedResponse<AnchorLinkComponent>> mappedApplicationLinks =
                request.FindManyDescendantsAndMap<AnchorLinkComponent>(
                    _mapRequestFactory,
                    new CssElementSelector(".govuk-footer__link"),
                    _anchorLinkMapper)
                .AddMappedResponseToResults(request.MappingResults);

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
            request.From);
    }
}
