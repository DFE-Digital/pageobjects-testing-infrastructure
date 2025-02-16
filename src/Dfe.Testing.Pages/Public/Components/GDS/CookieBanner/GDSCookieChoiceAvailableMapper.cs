using Dfe.Testing.Pages.Public.Components.Form;
using Dfe.Testing.Pages.Public.Components.Link;
using Dfe.Testing.Pages.Public.Components.Text;

namespace Dfe.Testing.Pages.Public.Components.GDS.CookieBanner;
internal sealed class GDSCookieChoiceAvailableMapper : IComponentMapper<GDSCookieChoiceAvailableBannerComponent>
{
    private readonly IMapRequestFactory _mapRequestFactory;
    private readonly IGDSCookieChoiceAvailableBannerComponentBuilder _builder;
    private readonly IMappingResultFactory _mappingResultFactory;
    private readonly IComponentMapper<FormComponentOld> _formMapper;
    private readonly IComponentMapper<AnchorLinkComponentOld> _anchorLinkMapper;
    private readonly IComponentMapper<TextComponent> _textMapper;

    public GDSCookieChoiceAvailableMapper(
        IMapRequestFactory mapRequestFactory,
        IGDSCookieChoiceAvailableBannerComponentBuilder builder,
        IMappingResultFactory mappingResultFactory,
        IComponentMapper<FormComponentOld> formMapper,
        IComponentMapper<AnchorLinkComponentOld> linkMapper,
        IComponentMapper<TextComponent> textMapper)
    {
        ArgumentNullException.ThrowIfNull(linkMapper);
        ArgumentNullException.ThrowIfNull(textMapper);
        _mapRequestFactory = mapRequestFactory;
        _builder = builder;
        _mappingResultFactory = mappingResultFactory;
        _formMapper = formMapper;
        _anchorLinkMapper = linkMapper;
        _textMapper = textMapper;
    }

    public MappedResponse<GDSCookieChoiceAvailableBannerComponent> Map(IMapRequest<IDocumentSection> request)
    {
        MappedResponse<TextComponent> mappedHeading =
            _textMapper.Map(
                _mapRequestFactory.CreateRequestFrom(request, nameof(GDSCookieChoiceAvailableBannerComponent.Heading)));

        MappedResponse<AnchorLinkComponentOld> mappedViewCookiesLink =
            _anchorLinkMapper.Map(
                _mapRequestFactory.CreateRequestFrom(request, nameof(GDSCookieChoiceAvailableBannerComponent.ViewCookiesLink)));

        MappedResponse<FormComponentOld> mappedForm =
            _formMapper.Map(
                _mapRequestFactory.CreateRequestFrom(request, nameof(GDSCookieChoiceAvailableBannerComponent.CookieChoiceForm)));

        _builder
            .SetViewCookiesLink(mappedViewCookiesLink.Mapped!)
            .SetHeading(mappedHeading.Mapped!.Text)
            .SetForm(mappedForm.Mapped!);

        MappedResponse<GDSCookieChoiceAvailableBannerComponent> mappedAvailableBanner =
            _mappingResultFactory.Create(
                request.Options.MapKey,
                _builder.Build(),
                MappingStatus.Success, // TODO predicate whether this component mapping was successful
                request.Document)
            .AddToMappingResults(mappedHeading.MappingResults)
            .AddToMappingResults(mappedForm.MappingResults)
            .AddToMappingResults(mappedViewCookiesLink.MappingResults);

        return mappedAvailableBanner;
    }
}
