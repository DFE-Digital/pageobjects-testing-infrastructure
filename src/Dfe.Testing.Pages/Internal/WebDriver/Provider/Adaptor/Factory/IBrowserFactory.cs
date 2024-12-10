namespace Dfe.Testing.Pages.Internal.WebDriver.Provider.Adaptor.Factory;
internal interface IBrowserFactory
{
    BrowserType Key { get; }
    Task<IWebDriver> Create(WebDriverSessionOptions sessionOptions);
}
