using Dfe.Testing.Pages.Public.Components.Text;

namespace Dfe.Testing.Pages.Public.Components.GDS.ErrorMessage;
internal sealed class GDSErrorMessageMapper : IComponentMapper<GDSErrorMessageComponent>
{
    private readonly IMapRequestFactory _mapRequestFactory;
    private readonly IMappingResultFactory _mappingResultFactory;
    private readonly IComponentMapper<TextComponent> _textMapper;

    public GDSErrorMessageMapper(
        IMapRequestFactory mapRequestFactory,
        IMappingResultFactory mappingResultFactory,
        IComponentMapper<TextComponent> textMapper)
    {
        _mapRequestFactory = mapRequestFactory;
        _mappingResultFactory = mappingResultFactory;
        _textMapper = textMapper;
    }

    public MappedResponse<GDSErrorMessageComponent> Map(IMapRequest<IDocumentSection> request)
    {
        MappedResponse<TextComponent> mappedErrorMessage =
            _textMapper.Map(
                _mapRequestFactory.CreateRequestFrom(request, nameof(GDSErrorMessageComponent.ErrorMessage)))
            .AddToMappingResults(request.MappedResults);

        GDSErrorMessageComponent component = new()
        {
            ErrorMessage = mappedErrorMessage.Mapped!
        };

        return _mappingResultFactory.Create(
            request.Options.MapKey,
            component,
            MappingStatus.Success,
            request.Document);
    }
}
