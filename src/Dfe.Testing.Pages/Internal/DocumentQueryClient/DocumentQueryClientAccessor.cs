namespace Dfe.Testing.Pages.Internal.DocumentQueryClient;

internal sealed class DocumentQueryClientAccessor : IDocumentQueryClientAccessor
{
    private IDocumentQueryClient? _documentQueryClient;
    public IDocumentQueryClient DocumentQueryClient
    {
        get => _documentQueryClient ?? throw new ArgumentNullException(nameof(_documentQueryClient));
        set => _documentQueryClient = value;
    }
}
