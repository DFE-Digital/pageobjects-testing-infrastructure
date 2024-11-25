namespace Dfe.Testing.Pages.Internal.DocumentQueryClient;

internal sealed class DocumentQueryClientAccessor : IDocumentQueryClientAccessor
{
    private IDocumentQueryClient? _documentQueryClient;
    IDocumentQueryClient IDocumentQueryClientAccessor.DocumentQueryClient
    {
        get => _documentQueryClient ?? throw new ArgumentNullException(nameof(_documentQueryClient));
        set => _documentQueryClient = value;
    }
}
