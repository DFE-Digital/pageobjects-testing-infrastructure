namespace Dfe.Testing.Pages.Public.DocumentQueryClient.Pages;

public sealed class PageFactory : IPageFactory
{
    private readonly IDictionary<string, Func<PageBase>> _pageFactory;
    public PageFactory(IDictionary<string, Func<PageBase>> pageFactory)
    {
        ArgumentNullException.ThrowIfNull(pageFactory);
        _pageFactory = pageFactory;
    }

    public PageBase CreatePage<TPage>() => CreatePage(typeof(TPage));

    public PageBase CreatePage(Type page) => CreatePage(pageName: page.Name);

    public PageBase CreatePage(string pageName)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(pageName);

        return !_pageFactory
            .TryGetValue(pageName, out var page) || page is null ?
                throw new ArgumentOutOfRangeException(
                    $"Page of type {pageName} is not registered.") : page();
    }
}
