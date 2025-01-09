using Dfe.Testing.Pages.Public.Components.MappingAbstraction.Request;
using Dfe.Testing.Pages.Public.Components.Text;

namespace Dfe.Testing.Pages.Public.Components.Label;
internal sealed class LabelMapper : IMapper<IMapRequest<IDocumentSection>, MappedResponse<LabelComponent>>
{
    private readonly IMapRequestFactory _mapRequestFactory;
    private readonly IMapper<IMapRequest<IDocumentSection>, MappedResponse<TextComponent>> _textMapper;
    private readonly IMappingResultFactory _mappingResultFactory;

    public LabelMapper(
        IMapRequestFactory mapRequestFactory,
        IMapper<IMapRequest<IDocumentSection>, MappedResponse<TextComponent>> textMapper,
        IMappingResultFactory mappingResultFactory)
    {
        _mapRequestFactory = mapRequestFactory;
        _textMapper = textMapper;
        _mappingResultFactory = mappingResultFactory;
    }

    public MappedResponse<LabelComponent> Map(IMapRequest<IDocumentSection> request)
    {
        MappedResponse<TextComponent> mappedText = _textMapper.Map(
            _mapRequestFactory.Create(
                request.From,
                request.MappingResults))
            .AddMappedResponseToResults(request.MappingResults);

        LabelComponent component = new()
        {
            For = request.From.GetAttribute("for") ?? string.Empty,
            Text = mappedText.Mapped
        };

        return _mappingResultFactory.Create(
            component,
            MappingStatus.Success,
            request.From);
    }
}
