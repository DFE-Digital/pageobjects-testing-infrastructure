namespace Dfe.Testing.Pages.Public.Components.Text;
internal sealed class TextMapper : IComponentMapper<TextComponent>
{
    private readonly IMappingResultFactory _mappingResultFactory;

    public TextMapper(IMappingResultFactory mappingResultFactory)
    {
        _mappingResultFactory = mappingResultFactory;
    }

    public MappedResponse<TextComponent> Map(IMapRequest<IDocumentSection> request)
    {
        TextComponent text = new()
        {
            Text = request.Document.Text ?? string.Empty
        };

        return _mappingResultFactory.Create(
            request.Options.MapKey,
            text,
            MappingStatus.Success,
            request.Document);
    }
}
