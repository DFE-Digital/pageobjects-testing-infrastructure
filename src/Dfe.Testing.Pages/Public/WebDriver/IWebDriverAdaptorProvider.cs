namespace Dfe.Testing.Pages.Public.WebDriver;
public interface IWebDriverAdaptorProvider
{
    Task<IWebDriverAdaptor> GetAsync();
}
