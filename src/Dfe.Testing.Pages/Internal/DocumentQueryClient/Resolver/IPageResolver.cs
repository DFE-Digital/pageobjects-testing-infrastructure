namespace Dfe.Testing.Pages.Internal.DocumentQueryClient.Resolver;
internal interface IPageObjectResolver
{
    TPage GetPage<TPage>() where TPage : class, IPage;
}
