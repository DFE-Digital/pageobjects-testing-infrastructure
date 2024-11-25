namespace Dfe.Testing.Pages.Internal.DocumentQueryClient;
internal interface IDocumentQueryClient
{
    internal void Run(QueryRequestArgs args, Action<IDocumentPart> handler);
    internal TResult Query<TResult>(QueryRequestArgs args, Func<IDocumentPart, TResult> mapper);
    internal IEnumerable<TResult> QueryMany<TResult>(QueryRequestArgs args, Func<IDocumentPart, TResult> mapper);
}
