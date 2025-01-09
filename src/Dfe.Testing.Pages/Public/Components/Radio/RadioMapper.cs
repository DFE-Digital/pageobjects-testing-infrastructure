using Dfe.Testing.Pages.Public.Components.Link;
using Dfe.Testing.Pages.Public.Components.MappingAbstraction.Request;

namespace Dfe.Testing.Pages.Public.Components.Radio;
internal sealed class RadioMapper : IMapper<IMapRequest<IDocumentSection>, MappedResponse<RadioComponent>>
{
    private readonly IMappingResultFactory _mappingResultFactory;

    public RadioMapper(IMappingResultFactory mappingResultFactory)
    {
        _mappingResultFactory = mappingResultFactory;
    }

    public MappedResponse<RadioComponent> Map(IMapRequest<IDocumentSection> request)
    {
        RadioComponent radio = new()
        {
            Id = request.From.GetAttribute("id") ?? string.Empty,
            Value = request.From.GetAttribute("value") ?? string.Empty,
            Name = request.From.GetAttribute("name") ?? string.Empty,
            IsRequired = request.From.HasAttribute("required")
        };

        return _mappingResultFactory.Create(
            radio,
            MappingStatus.Success,
            request.From);
    }
}
