using System.ComponentModel;
using Dfe.Testing.Pages.Public.Components.GDS.Button;
using Dfe.Testing.Pages.Public.Components.GDS.Checkbox;
using Dfe.Testing.Pages.Public.Components.Link;
using Dfe.Testing.Pages.Public.Components.MappingAbstraction.Request;
using Dfe.Testing.Pages.Public.Components.SelectorFactory;
using Dfe.Testing.Pages.Public.Components.Text;

namespace Dfe.Testing.Pages.Public.Components.GDS.CookieBanner;
internal sealed class GDSCookieChoiceMadeBannerMappper : IMapper<IMapRequest<IDocumentSection>, MappedResponse<GDSCookieChoiceMadeBannerComponent>>
{
    private readonly IGDSCookieChoiceMadeBannerComponentBuilder _builder;
    private readonly IMapRequestFactory _mapRequestFactory;
    private readonly IMappingResultFactory _mappingResultFactory;
    private readonly IMapper<IMapRequest<IDocumentSection>, MappedResponse<TextComponent>> _textMapper;
    private readonly IMapper<IMapRequest<IDocumentSection>, MappedResponse<AnchorLinkComponent>> _linkMapper;
    private readonly IMapper<IMapRequest<IDocumentSection>, MappedResponse<GDSButtonComponent>> _buttonMapper;

    public GDSCookieChoiceMadeBannerMappper(
        IGDSCookieChoiceMadeBannerComponentBuilder builder,
        IMapRequestFactory mapRequestFactory,
        IMappingResultFactory mappingResultFactory,
        IMapper<IMapRequest<IDocumentSection>, MappedResponse<TextComponent>> textFactory,
        IMapper<IMapRequest<IDocumentSection>, MappedResponse<AnchorLinkComponent>> linkFactory,
        IMapper<IMapRequest<IDocumentSection>, MappedResponse<GDSButtonComponent>> buttonFactory)
    {
        _mappingResultFactory = mappingResultFactory;
        _textMapper = textFactory;
        _linkMapper = linkFactory;
        _buttonMapper = buttonFactory;
        _builder = builder;
        _mapRequestFactory = mapRequestFactory;
    }

    public MappedResponse<GDSCookieChoiceMadeBannerComponent> Map(IMapRequest<IDocumentSection> request)
    {
        MappedResponse<TextComponent> mappedMessage = _textMapper.Map(
            _mapRequestFactory.Create(
                request.From,
                request.MappingResults,
                new CssElementSelector(".govuk-cookie-banner_content")))
            .AddMappedResponseToResults(request.MappingResults);

        MappedResponse<AnchorLinkComponent> mappedChangeYourCookiesLink = _linkMapper.Map(
            _mapRequestFactory.Create(
                request.From,
                request.MappingResults))
            .AddMappedResponseToResults(request.MappingResults);

        MappedResponse<GDSButtonComponent> mappedHideCookiesButton = _buttonMapper.Map(
            _mapRequestFactory.Create(
                request.From,
                request.MappingResults))
            .AddMappedResponseToResults(request.MappingResults);

        _builder.SetHideCookiesButton(mappedHideCookiesButton.Mapped!)
            .SetChangeYourCookieSettingsLink(mappedChangeYourCookiesLink.Mapped!)
            .SetMessage(mappedMessage.Mapped!.Text);

        return _mappingResultFactory.Create(
            _builder.Build(),
            MappingStatus.Success,
            request.From);
    }
}
