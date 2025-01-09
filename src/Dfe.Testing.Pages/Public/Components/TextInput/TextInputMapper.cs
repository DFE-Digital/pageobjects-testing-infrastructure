using Dfe.Testing.Pages.Public.Components.Link;
using Dfe.Testing.Pages.Public.Components.MappingAbstraction.Request;

namespace Dfe.Testing.Pages.Public.Components.Inputs;
internal sealed class TextInputMapper : IMapper<IMapRequest<IDocumentSection>, MappedResponse<TextInputComponent>>
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
            Value = request.From?.GetAttribute("value") ?? string.Empty,
            Name = request.From?.GetAttribute("name") ?? string.Empty,
            Type = request.From?.GetAttribute("type") ?? string.Empty,
            PlaceHolder = request.From?.GetAttribute("placeholder") ?? string.Empty,
            IsRequired = request.From?.HasAttribute("required") ?? false,
            AutoComplete = request.From?.GetAttribute("autocomplete") ?? string.Empty
        };

        return _mappingResultFactory.Create(
            component,
            MappingStatus.Success,
            request.From!);
    }
}
