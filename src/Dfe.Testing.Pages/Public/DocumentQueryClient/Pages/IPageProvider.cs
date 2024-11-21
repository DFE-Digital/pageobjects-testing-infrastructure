namespace Dfe.Testing.Pages.Public.DocumentQueryClient.Pages;
public interface IPageProvider
{
    public Task<PageBase> CreatePageAsync<TPage>(HttpRequestMessage httpRequest) where TPage : PageBase;
}
