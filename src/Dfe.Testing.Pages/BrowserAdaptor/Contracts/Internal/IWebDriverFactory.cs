namespace Dfe.Testing.Pages.BrowserAdaptor.Contracts.Internal;
internal interface IWebDriverFactory<TDriver> where TDriver : class, IWebDriver
{
    Task<TDriver> CreateDriverAsync(BrowserStartSessionRequest options);
}
