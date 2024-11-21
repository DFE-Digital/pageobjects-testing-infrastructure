namespace Dfe.Testing.Pages.Internal.WebDriver.Provider;
internal interface IWebDriverAdaptorProvider
{
    Task<IWebDriverAdaptor> CreateAsync();
}