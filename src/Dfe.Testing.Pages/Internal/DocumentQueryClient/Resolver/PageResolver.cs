namespace Dfe.Testing.Pages.Internal.DocumentQueryClient.Resolver;
internal sealed class PageObjectResolver : IPageObjectResolver
{
    private readonly IEnumerable<IPage> _pages;

    public PageObjectResolver(
        IEnumerable<IPage> pages)
    {
        ArgumentNullException.ThrowIfNull(pages);
        _pages = pages;
    }

    public TPage GetPage<TPage>() where TPage : class, IPage
        => (TPage)_pages.Single((page) => page.GetType().Name == typeof(TPage).Name);
}
