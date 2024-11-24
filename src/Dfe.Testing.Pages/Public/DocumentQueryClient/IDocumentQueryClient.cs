namespace Dfe.Testing.Pages.Public.DocumentQueryClient;
internal interface IDocumentQueryClient
{
    void Run(QueryRequestArgs args, Action<IDocumentPart> handler);
    TResult Query<TResult>(QueryRequestArgs args, Func<IDocumentPart, TResult> mapper);
    IEnumerable<TResult> QueryMany<TResult>(QueryRequestArgs args, Func<IDocumentPart, TResult> mapper);
}
