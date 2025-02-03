using Dfe.Testing.Pages.Public.Components.Form;
using Dfe.Testing.Pages.Public.Components.Link;
using Dfe.Testing.Pages.Public.Components.Text;

namespace Dfe.Testing.Pages.Public.Components.GDS.CookieBanner;
internal sealed class GDSCookieChoiceMadeBannerComponentMapper : IComponentMapper<GDSCookieChoiceMadeBannerComponent>
{
    private readonly IGDSCookieChoiceMadeBannerComponentBuilder _gdsCookieChoiceMadeBannerBuilder;
    private readonly IMappingResultFactory _mappingResultFactory;
    private readonly IComponentMapper<TextComponent> _textMapper;
    private readonly IComponentMapper<AnchorLinkComponent> _linkMapper;
    private readonly IComponentMapper<FormComponent> _formMapper;
    private readonly IMapRequestFactory _mapRequestFactory;

    public GDSCookieChoiceMadeBannerComponentMapper(
        IGDSCookieChoiceMadeBannerComponentBuilder builder,
        IMappingResultFactory mappingResultFactory,
        IComponentMapper<TextComponent> textMapper,
        IComponentMapper<AnchorLinkComponent> linkMapper,
        IComponentMapper<FormComponent> formMapper,
        IMapRequestFactory mapRequestFactory)
    {
        _mappingResultFactory = mappingResultFactory;
        _textMapper = textMapper;
        _linkMapper = linkMapper;
        _formMapper = formMapper;
        _gdsCookieChoiceMadeBannerBuilder = builder;
        _mapRequestFactory = mapRequestFactory;
    }

    public MappedResponse<GDSCookieChoiceMadeBannerComponent> Map(IMapRequest<IDocumentSection> request)
    {
        MappedResponse<TextComponent> mappedMessage =
            _textMapper.Map(
                _mapRequestFactory.CreateRequestFrom(request, nameof(GDSCookieChoiceMadeBannerComponent.Message)))
            .AddToMappingResults(request.MappedResults);

        MappedResponse<AnchorLinkComponent> mappedChangeYourCookiesLink =
            _linkMapper.Map(
                _mapRequestFactory.CreateRequestFrom(request, nameof(GDSCookieChoiceMadeBannerComponent.ChangeYourCookieSettingsLink)))
            .AddToMappingResults(request.MappedResults);

        MappedResponse<FormComponent> mappedForm =
            _formMapper.Map(
                _mapRequestFactory.CreateRequestFrom(request, nameof(GDSCookieChoiceMadeBannerComponent.HideCookiesForm)))
            .AddToMappingResults(request.MappedResults);

        _gdsCookieChoiceMadeBannerBuilder
            .SetForm(mappedForm.Mapped!)
            .SetChangeYourCookieSettingsLink(mappedChangeYourCookiesLink.Mapped!)
            .SetMessage(mappedMessage.Mapped!.Text);

        return _mappingResultFactory.Create(
            _gdsCookieChoiceMadeBannerBuilder.Build(),
            MappingStatus.Success,
            request.Document);
    }
}
