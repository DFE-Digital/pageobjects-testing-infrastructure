namespace Dfe.Testing.Pages.Internal.DocumentQueryClient.Resolver;
internal sealed class PageObjectResolver : IPageObjectResolver
{
    private readonly IEnumerable<IPageObject> _pages;

    public PageObjectResolver(
        IEnumerable<IPageObject> pages)
    {
        ArgumentNullException.ThrowIfNull(pages);
        _pages = pages;
    }

    public TPage GetPage<TPage>() where TPage : class, IPageObject
        => (TPage)_pages.Single((page) => page.GetType().Name == typeof(TPage).Name);
}
