using Dfe.Testing.Pages.Public.Components.Text;
using Dfe.Testing.Pages.Public.Components.MappingAbstraction.Request;

namespace Dfe.Testing.Pages.Public.Components.GDS.ErrorMessage;
internal sealed class GDSErrorMessageMapper : IMapper<IMapRequest<IDocumentSection>, MappedResponse<GDSErrorMessageComponent>>
{
    private readonly IMapRequestFactory _mapRequestFactory;
    private readonly IMappingResultFactory _mappingResultFactory;
    private readonly IMapper<IMapRequest<IDocumentSection>, MappedResponse<TextComponent>> _textMapper;

    public GDSErrorMessageMapper(
        IMapRequestFactory mapRequestFactory,
        IMappingResultFactory mappingResultFactory,
        IMapper<IMapRequest<IDocumentSection>, MappedResponse<TextComponent>> textMapper)
    {
        _mappingResultFactory = mappingResultFactory;
        _textMapper = textMapper;
        _mapRequestFactory = mapRequestFactory;
    }

    public MappedResponse<GDSErrorMessageComponent> Map(IMapRequest<IDocumentSection> request)
    {
        MappedResponse<TextComponent> mappedErrorMessage = _textMapper.Map(
            _mapRequestFactory.Create(
                request.From,
                request.MappingResults,
                new CssElementSelector(".govuk-error-message")));

        GDSErrorMessageComponent component = new()
        {
            ErrorMessage = mappedErrorMessage.Mapped!
        };

        return _mappingResultFactory.Create(
            component,
            MappingStatus.Success,
            request.From);
    }
}
