namespace Dfe.Testing.Pages.Public.Components.TextInput;
internal sealed class TextInputMapper : IComponentMapper<TextInputComponent>
{
    private readonly IMappingResultFactory _mappingResultFactory;

    public TextInputMapper(IMappingResultFactory mappingResultFactory)
    {
        _mappingResultFactory = mappingResultFactory;
    }

    public MappedResponse<TextInputComponent> Map(IMapRequest<IDocumentSection> request)
    {
        TextInputComponent component = new()
        {
            Value = request.Document?.GetAttribute("value") ?? string.Empty,
            Name = request.Document?.GetAttribute("name") ?? string.Empty,
            Type = request.Document?.GetAttribute("type") ?? string.Empty,
            PlaceHolder = request.Document?.GetAttribute("placeholder") ?? string.Empty,
            IsRequired = request.Document?.HasAttribute("required") ?? false,
            AutoComplete = request.Document?.GetAttribute("autocomplete") ?? string.Empty
        };

        return _mappingResultFactory.Create(
            component,
            MappingStatus.Success,
            request.Document!);
    }
}
