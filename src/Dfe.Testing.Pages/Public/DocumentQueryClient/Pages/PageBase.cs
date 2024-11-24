namespace Dfe.Testing.Pages.Public.DocumentQueryClient.Pages;
public abstract class PageBase
{
    private readonly IDocumentQueryClientAccessor _documentQueryClientAccessor;
    internal PageBase(IDocumentQueryClientAccessor documentQueryClientAccessor)
    {
        ArgumentNullException.ThrowIfNull(documentQueryClientAccessor, nameof(documentQueryClientAccessor));
        _documentQueryClientAccessor = documentQueryClientAccessor;
    }

    internal IDocumentQueryClient DocumentQueryClient
    {
        get => _documentQueryClientAccessor.DocumentQueryClient;
    }
}
