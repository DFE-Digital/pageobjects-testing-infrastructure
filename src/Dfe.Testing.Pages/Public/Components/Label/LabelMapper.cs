using Dfe.Testing.Pages.Public.Components.Text;

namespace Dfe.Testing.Pages.Public.Components.Label;
internal sealed class LabelMapper : IComponentMapper<LabelComponent>
{
    private readonly IMapRequestFactory _mapRequestFactory;
    private readonly IComponentMapper<TextComponent> _textMapper;
    private readonly IMappingResultFactory _mappingResultFactory;

    public LabelMapper(
        IMapRequestFactory mapRequestFactory,
        IComponentMapper<TextComponent> textMapper,
        IMappingResultFactory mappingResultFactory)
    {
        _mapRequestFactory = mapRequestFactory;
        _textMapper = textMapper;
        _mappingResultFactory = mappingResultFactory;
    }

    public MappedResponse<LabelComponent> Map(IMapRequest<IDocumentSection> request)
    {
        MappedResponse<TextComponent> mappedText = _textMapper.Map(
            _mapRequestFactory.CreateRequestFrom(request, nameof(LabelComponent.Text)))
                .AddToMappingResults(request.MappedResults);

        LabelComponent component = new()
        {
            For = request.Document.GetAttribute("for") ?? string.Empty,
            Text = mappedText.Mapped
        };

        return _mappingResultFactory.Create(
            component,
            MappingStatus.Success,
            request.Document);
    }
}
