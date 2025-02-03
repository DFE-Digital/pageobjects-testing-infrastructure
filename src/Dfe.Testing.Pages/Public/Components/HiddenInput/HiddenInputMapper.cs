namespace Dfe.Testing.Pages.Public.Components.HiddenInput;
internal sealed class HiddenInputMapper : IComponentMapper<HiddenInputComponent>
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
            Name = request.Document.GetAttribute("name") ?? string.Empty,
            Value = request.Document.GetAttribute("value") ?? string.Empty
        };

        return _mappingResultFactory.Create(
            hiddenInputComponent,
            MappingStatus.Success,
            request.Document);
    }
}
