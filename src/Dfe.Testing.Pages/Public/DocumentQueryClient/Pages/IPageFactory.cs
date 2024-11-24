namespace Dfe.Testing.Pages.Public.DocumentQueryClient.Pages;
public interface IPageFactory
{
    public Task<TPage> CreatePageAsync<TPage>(HttpRequestMessage httpRequest) where TPage : class, IPage;
}
