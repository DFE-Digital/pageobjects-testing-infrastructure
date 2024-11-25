namespace Dfe.Testing.Pages.Public;
public interface IPageFactory
{
    public Task<TPage> CreatePageAsync<TPage>(HttpRequestMessage httpRequest) where TPage : class, IPage;
}
