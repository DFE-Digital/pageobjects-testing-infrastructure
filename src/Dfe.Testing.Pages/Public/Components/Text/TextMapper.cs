using Dfe.Testing.Pages.Public.Components.MappingAbstraction.Request;

namespace Dfe.Testing.Pages.Public.Components.Text;
internal sealed class TextMapper : IMapper<IMapRequest<IDocumentSection>, MappedResponse<TextComponent>>
{
    private readonly IMapRequestFactory _mapRequestFactory;
    private readonly IMappingResultFactory _mappingResultFactory;

    public TextMapper(
        IMapRequestFactory mapRequestFactory,
        IMappingResultFactory mappingResultFactory)
    {
        _mappingResultFactory = mappingResultFactory;
        _mapRequestFactory = mapRequestFactory;
    }
    public MappedResponse<TextComponent> Map(IMapRequest<IDocumentSection> request)
    {
        TextComponent text = new()
        {
            Text = request.From.Text ?? string.Empty
        };

        return _mappingResultFactory.Create(
            text,
            MappingStatus.Success,
            request.From);
    }
}
