using Dfe.Testing.Pages.Public.Components.SelectorFactory;

namespace Dfe.Testing.Pages.Public.Components.MappingAbstraction.Request;
internal static class MapRequestExtensions
{
    internal static IEnumerable<MappedResponse<TOut>> FindManyDescendantsAndMap<TOut>(
        this IMapRequest<IDocumentSection> request,
        IMapRequestFactory mapRequestFactory,
        IElementSelector selector,
        IMapper<IMapRequest<IDocumentSection>, MappedResponse<TOut>> mapper) where TOut : class
    {
        return request.FindManyDescendants(selector)
            .Select((componentSection)
                => mapper.Map(
                    mapRequestFactory.Create(componentSection, request.MappingResults)));
    }

    private static IEnumerable<IDocumentSection> FindManyDescendants<T>(this IMapRequest<IDocumentSection> request, IComponentSelectorFactory selector) where T : class
        => FindManyDescendants(request, selector.GetSelector<T>());

    private static IEnumerable<IDocumentSection> FindManyDescendants(this IMapRequest<IDocumentSection> request, IElementSelector selector)
    {
        ArgumentNullException.ThrowIfNull(request);
        ArgumentNullException.ThrowIfNull(request.From);
        ArgumentNullException.ThrowIfNull(selector);
        return request.From.FindDescendants(selector);
    }
}
