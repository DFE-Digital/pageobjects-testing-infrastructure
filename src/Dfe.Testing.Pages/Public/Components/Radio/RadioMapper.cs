namespace Dfe.Testing.Pages.Public.Components.Radio;
internal sealed class RadioMapper : IComponentMapper<RadioComponent>
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
            Id = request.Document.GetAttribute("id") ?? string.Empty,
            Value = request.Document.GetAttribute("value") ?? string.Empty,
            Name = request.Document.GetAttribute("name") ?? string.Empty,
            IsRequired = request.Document.HasAttribute("required")
        };

        return _mappingResultFactory.Create(
            request.Options.MapKey,
            radio,
            MappingStatus.Success,
            request.Document);
    }
}
