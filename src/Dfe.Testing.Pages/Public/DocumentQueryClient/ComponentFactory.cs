namespace Dfe.Testing.Pages.Public.DocumentQueryClient.Pages;
public abstract class ComponentFactory<T> where T : IComponent
{
    private readonly IDocumentQueryClientAccessor _documentQueryClientAccessor;

    internal ComponentFactory(IDocumentQueryClientAccessor documentQueryClientAccessor)
    {
        ArgumentNullException.ThrowIfNull(documentQueryClientAccessor);
        _documentQueryClientAccessor = documentQueryClientAccessor;
    }

    internal IDocumentQueryClient DocumentQueryClient => _documentQueryClientAccessor.DocumentQueryClient;
    internal virtual QueryRequestArgs MergeRequest(QueryRequestArgs? request, IElementSelector defaultFindBySelector)
    {
        ArgumentNullException.ThrowIfNull(defaultFindBySelector);
        return new()
        {
            Query = request?.Query ?? defaultFindBySelector,
            Scope = request?.Scope
        };
    }
    public virtual T Get(QueryRequestArgs? request = null) => GetMany(request).Single();
    public abstract List<T> GetMany(QueryRequestArgs? request = null);
}
