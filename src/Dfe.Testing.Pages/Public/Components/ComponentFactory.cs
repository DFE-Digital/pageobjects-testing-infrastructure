using Dfe.Testing.Pages.Internal.DocumentClient;
using Dfe.Testing.Pages.Public.Components.SelectorFactory;

namespace Dfe.Testing.Pages.Public.Components;
public class ComponentFactory<T> where T : class
{
    private readonly IComponentSelectorFactory _componentSelectorFactory;
    private readonly IDocumentService _documentClient;
    private readonly IMapper<IDocumentSection, T> _mapper;

    public ComponentFactory(
        IDocumentService documentClient,
        IMapper<IDocumentSection, T> mapper,
        IComponentSelectorFactory componentSelectorFactory)
    {
        _documentClient = documentClient;
        _mapper = mapper;
        _componentSelectorFactory = componentSelectorFactory;
    }

    // TODO expand CreateComponentRequest to include ComponentMappingOptions

    public virtual T Get(CreateComponentRequest? request = null) => GetMany(request).Single();

    public virtual IList<T> GetMany(CreateComponentRequest? request = null)
    {
        var mergedOptions = DefaultQueryOptions(new()
        {
            FindInScope = request?.FindInScope ?? null,
            Selector = request?.Selector ?? null
        });

        return _documentClient.ExecuteQuery(mergedOptions)
            .Select(_mapper.Map)
                .ToList();
    }

    private FindOptions DefaultQueryOptions(FindOptions? options) => new()
    {
        Selector = options?.Selector ?? _componentSelectorFactory.GetSelector<T>(),
        FindInScope = options?.FindInScope ?? null
    };
}
