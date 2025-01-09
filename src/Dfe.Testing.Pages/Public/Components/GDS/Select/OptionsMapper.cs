using Dfe.Testing.Pages.Public.Components.MappingAbstraction.Request;
using Dfe.Testing.Pages.Public.Components.Text;

namespace Dfe.Testing.Pages.Public.Components.GDS.Select;
internal sealed class OptionsMapper : IMapper<IMapRequest<IDocumentSection>, MappedResponse<OptionComponent>>
{
    private readonly IMapRequestFactory _mapRequestFactory;
    private readonly IMapper<IMapRequest<IDocumentSection>, MappedResponse<TextComponent>> _textMapper;
    private readonly IMappingResultFactory _mappingResultFactory;

    public OptionsMapper(
        IMapRequestFactory mapRequestFactory,
        IMappingResultFactory mappingResultFactory,
        IMapper<IMapRequest<IDocumentSection>, MappedResponse<TextComponent>> textMapper)
    {
        _textMapper = textMapper;
        _mappingResultFactory = mappingResultFactory;
        _mapRequestFactory = mapRequestFactory;
    }
    public MappedResponse<OptionComponent> Map(IMapRequest<IDocumentSection> request)
    {
        MappedResponse<TextComponent> mappedText = _textMapper.Map(
            _mapRequestFactory.Create(
                request.From,
                request.MappingResults))
            .AddMappedResponseToResults(request.MappingResults);

        OptionComponent option = new()
        {
            IsSelected = request.From.HasAttribute("selected"),
            Text = mappedText.Mapped,
            Value = request.From.GetAttribute("value") ?? string.Empty
        };

        return _mappingResultFactory.Create(
            option,
            MappingStatus.Success,
            request.From);
    }
}
