using Dfe.Testing.Pages.Public.Components.GDS.Button;
using Dfe.Testing.Pages.Public.Components.Link;
using Dfe.Testing.Pages.Public.Components.MappingAbstraction.Request;
using Dfe.Testing.Pages.Public.Components.SelectorFactory;
using Dfe.Testing.Pages.Public.Components.Text;

namespace Dfe.Testing.Pages.Public.Components.GDS.CookieBanner;
internal sealed class GDSCookieChoiceAvailableMapper : IMapper<IMapRequest<IDocumentSection>, MappedResponse<GDSCookieChoiceAvailableBannerComponent>>
{
    private readonly IGDSCookieChoiceAvailableBannerComponentBuilder _builder;
    private readonly IMapRequestFactory _mapRequestFactory;
    private readonly IMappingResultFactory _mappingResultFactory;
    private readonly IComponentSelectorFactory _componentSelectorFactory;
    private readonly IMapper<IMapRequest<IDocumentSection>, MappedResponse<GDSButtonComponent>> _buttonMapper;
    private readonly IMapper<IMapRequest<IDocumentSection>, MappedResponse<AnchorLinkComponent>> _anchorLinkMapper;
    private readonly IMapper<IMapRequest<IDocumentSection>, MappedResponse<TextComponent>> _textMapper;

    public GDSCookieChoiceAvailableMapper(
        IGDSCookieChoiceAvailableBannerComponentBuilder builder,
        IMapRequestFactory mapRequestFactory,
        IMappingResultFactory mappingResultFactory,
        IComponentSelectorFactory componentSelectorFactory,
        IMapper<IMapRequest<IDocumentSection>, MappedResponse<GDSButtonComponent>> buttonMapper,
        IMapper<IMapRequest<IDocumentSection>, MappedResponse<AnchorLinkComponent>> linkMapper,
        IMapper<IMapRequest<IDocumentSection>, MappedResponse<TextComponent>> textMapper)
    {
        ArgumentNullException.ThrowIfNull(buttonMapper);
        ArgumentNullException.ThrowIfNull(linkMapper);
        ArgumentNullException.ThrowIfNull(textMapper);
        _builder = builder;
        _mapRequestFactory = mapRequestFactory;
        _mappingResultFactory = mappingResultFactory;
        _componentSelectorFactory = componentSelectorFactory;
        _buttonMapper = buttonMapper;
        _anchorLinkMapper = linkMapper;
        _textMapper = textMapper;
    }

    public MappedResponse<GDSCookieChoiceAvailableBannerComponent> Map(IMapRequest<IDocumentSection> request)
    {
        ArgumentNullException.ThrowIfNull(request);
        ArgumentNullException.ThrowIfNull(request.From);

        MappedResponse<TextComponent> mappedHeading = _textMapper.Map(
            _mapRequestFactory.Create(
                request.From,
                request.MappingResults,
                new CssElementSelector(".govuk-cookie-banner__heading")));

        IEnumerable<MappedResponse<GDSButtonComponent>> mappedCookieChoiceButtons
            = request.FindManyDescendantsAndMap<GDSButtonComponent>(
                _mapRequestFactory,
                _componentSelectorFactory.GetSelector<GDSButtonComponent>(),
                _buttonMapper);

        MappedResponse<AnchorLinkComponent> mappedViewCookiesLink = _anchorLinkMapper.Map(
            _mapRequestFactory.Create(
                request.From,
                request.MappingResults,
                _componentSelectorFactory.GetSelector<AnchorLinkComponent>()));

        _builder
            .SetViewCookiesLink(mappedViewCookiesLink.Mapped!)
            .SetHeading(mappedHeading.Mapped!.Text);

        mappedCookieChoiceButtons.Select(t => t.Mapped!)
            .ToList()
            .ForEach(button => _builder.AddCookieChoiceButton(button));

        return _mappingResultFactory.Create(
            _builder.Build(),
            MappingStatus.Success,
            request.From);
    }
}
