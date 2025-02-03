using Dfe.Testing.Pages.Public.Components.Text;

namespace Dfe.Testing.Pages.Public.Components.GDS.Select;
internal sealed class OptionsMapper : IComponentMapper<OptionComponent>
{
    private readonly IComponentMapper<TextComponent> _textMapper;
    private readonly IMapRequestFactory _mapRequestFactory;
    private readonly IMappingResultFactory _mappingResultFactory;

    public OptionsMapper(
        IMapRequestFactory mapRequestFactory,
        IMappingResultFactory mappingResultFactory,
        IComponentMapper<TextComponent> textMapper)
    {
        _textMapper = textMapper;
        _mapRequestFactory = mapRequestFactory;
        _mappingResultFactory = mappingResultFactory;
    }

    public MappedResponse<OptionComponent> Map(IMapRequest<IDocumentSection> request)
    {
        MappedResponse<TextComponent> mappedText = _textMapper.Map(
            _mapRequestFactory.CreateRequestFrom(request, nameof(OptionComponent.Text)))
                .AddToMappingResults(request.MappedResults);

        OptionComponent option = new()
        {
            IsSelected = request.Document.HasAttribute("selected"),
            Text = mappedText.Mapped,
            Value = request.Document.GetAttribute("value") ?? string.Empty
        };

        return _mappingResultFactory.Create(
            option,
            MappingStatus.Success,
            request.Document);
    }
}
