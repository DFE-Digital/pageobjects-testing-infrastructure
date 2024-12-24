namespace Dfe.Testing.Pages.Public.PageObject;
internal sealed class PageObjectFactory : IPageObjectFactory
{
    private readonly IEnumerable<IPageObject> _pages;

    public PageObjectFactory(
        IEnumerable<IPageObject> pages)
    {
        ArgumentNullException.ThrowIfNull(pages);
        _pages = pages;
    }

    public TPage GetPage<TPage>() where TPage : class, IPageObject
        => (TPage)_pages.Single((page) =>
            page.GetType().Name == typeof(TPage).Name);
}
