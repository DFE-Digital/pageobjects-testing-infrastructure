namespace Dfe.Testing.Pages.Public.Components.MappingAbstraction.Request;
internal static class MapRequestExtensions
{
    internal static IEnumerable<MappedResponse<TOut>> FindManyDescendantsAndMapToComponent<TOut>(
        this IMapRequest<IDocumentSection> request,
        IMapRequestFactory requestFactory,
        IMapper<IMapRequest<IDocumentSection>, MappedResponse<TOut>> mapper,
        IElementSelector? overrideEntrypointForComponent = null) where TOut : class
    {
        ArgumentNullException.ThrowIfNull(request);
        ArgumentNullException.ThrowIfNull(request.Document);
        return request.Document.FindDescendants(
                overrideEntrypointForComponent ??
                request.Options.OverrideMapperEntrypoint ??
                    throw new ArgumentException("unable to get entrypoint for descendants from request inputs"))
            .Select((descendantDocumentSection) => requestFactory.CreateRequestWithDocumentFrom(request, descendantDocumentSection))
            .Select(mapper.Map);
    }
}
