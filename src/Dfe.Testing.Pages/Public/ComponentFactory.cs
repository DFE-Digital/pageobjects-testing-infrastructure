using Dfe.Testing.Pages.Components;
using Dfe.Testing.Pages.Public.Mapper.Abstraction;
using Dfe.Testing.Pages.Public.Selector.Factory;

namespace Dfe.Testing.Pages.Public;
public class ComponentFactory<T> where T : IComponent
{
    private readonly IComponentSelectorFactory _componentSelectorFactory;
    private readonly IDocumentQueryClientAccessor _documentQueryClientAccessor;
    private readonly IComponentMapper<T> _mapper;

    public ComponentFactory(
        IComponentSelectorFactory componentSelectorFactory,
        IDocumentQueryClientAccessor documentQueryClientAccessor,
        IComponentMapper<T> mapper)
    {
        ArgumentNullException.ThrowIfNull(documentQueryClientAccessor);
        _componentSelectorFactory = componentSelectorFactory;
        _documentQueryClientAccessor = documentQueryClientAccessor;
        _mapper = mapper;
    }

    internal IDocumentQueryClient DocumentQueryClient => _documentQueryClientAccessor.DocumentQueryClient;
    internal virtual QueryOptions MergeRequest(QueryOptions? request)
    {
        return new()
        {
            // fall back to using the default selector for the component if no query is provided
            // Entrypoint for the top-level component
            Query = request?.Query ?? _componentSelectorFactory.GetSelector<T>(), // KeyValuePair <this, T>
            // Inside of this area , we're looking for this component
            InScope = request?.InScope
        };
    }

    public virtual T Get(QueryOptions? request = null) => GetMany(request).Single();

    public virtual IList<T> GetMany(QueryOptions? request = null)
    {
        return DocumentQueryClient.QueryMany(args: MergeRequest(request))
                .Select(_mapper.Map)
                .ToList();
    }

    //TODO consider removing below - enforcing client to pass QueryOptions mappings

    internal virtual IList<T> GetManyFromPart(IDocumentPart? part)
        => part?
            .GetChildren(_componentSelectorFactory.GetSelector<T>())?
            .Select(_mapper.Map)
            .ToList() ?? throw new ArgumentNullException(nameof(part));

}
