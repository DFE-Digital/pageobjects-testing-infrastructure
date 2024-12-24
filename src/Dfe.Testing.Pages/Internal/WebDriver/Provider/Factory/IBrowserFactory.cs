namespace Dfe.Testing.Pages.Internal.WebDriver.Provider.Factory;
internal interface IBrowserFactory
{
    BrowserType Key { get; }
    Task<IWebDriver> Create(WebDriverSessionOptions sessionOptions);
}
