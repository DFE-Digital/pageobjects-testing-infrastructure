namespace Dfe.Testing.Pages.Internal.DocumentQueryClient;
public interface IDocumentQueryClientAccessor
{
    internal IDocumentQueryClient DocumentQueryClient { get; set; }
}
