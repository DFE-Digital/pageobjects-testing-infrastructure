namespace Dfe.Testing.Pages.Public.DocumentQueryClient;
public interface IPageFactory
{
    public Task<TPage> CreatePageAsync<TPage>(HttpRequestMessage httpRequest) where TPage : class, IPage;
}
