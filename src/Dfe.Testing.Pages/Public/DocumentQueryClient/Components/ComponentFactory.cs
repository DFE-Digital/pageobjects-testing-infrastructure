namespace Dfe.Testing.Pages.Public.DocumentQueryClient.Components;
public abstract class ComponentFactory<T> where T : IComponent
{
    private readonly IDocumentQueryClientAccessor _documentQueryClientAccessor;

    internal ComponentFactory(IDocumentQueryClientAccessor documentQueryClientAccessor)
    {
        ArgumentNullException.ThrowIfNull(documentQueryClientAccessor);
        _documentQueryClientAccessor = documentQueryClientAccessor;
    }

    internal IDocumentQueryClient DocumentQueryClient => _documentQueryClientAccessor.DocumentQueryClient;
    public virtual T Get(QueryRequest? request = null) => GetMany(request).Single();
    public abstract List<T> GetMany(QueryRequest? request = null);
}
