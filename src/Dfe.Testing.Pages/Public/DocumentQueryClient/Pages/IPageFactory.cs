namespace Dfe.Testing.Pages.Public.DocumentQueryClient.Pages;
public interface IPageFactory
{
    public PageBase CreatePage<TPage>();
    public PageBase CreatePage(Type page);
    public PageBase CreatePage(string pageName);
}
