using Dfe.Testing.Pages.Public.Components.GDS.Table.TableRow;
using Dfe.Testing.Pages.Public.Components.MappingAbstraction.Request;

namespace Dfe.Testing.Pages.Public.Components.HiddenInput;
internal sealed class HiddenInputMapper : IMapper<IMapRequest<IDocumentSection>, MappedResponse<HiddenInputComponent>>
{
    private readonly IMappingResultFactory _mappingResultFactory;

    public HiddenInputMapper(IMappingResultFactory mappingResultFactory)
    {
        _mappingResultFactory = mappingResultFactory;
    }

    public MappedResponse<HiddenInputComponent> Map(IMapRequest<IDocumentSection> request)
    {

        HiddenInputComponent hiddenInputComponent = new()
        {
            Name = request.From.GetAttribute("name") ?? string.Empty,
            Value = request.From.GetAttribute("value") ?? string.Empty
        };

        return _mappingResultFactory.Create(
            hiddenInputComponent,
            MappingStatus.Success,
            request.From);
    }
}
