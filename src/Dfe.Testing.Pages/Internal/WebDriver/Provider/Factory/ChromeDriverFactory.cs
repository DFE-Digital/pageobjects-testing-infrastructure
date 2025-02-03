using OpenQA.Selenium.Chrome;

namespace Dfe.Testing.Pages.Internal.WebDriver.Provider.Factory;

internal sealed class ChromeDriverFactory : IBrowserFactory
{
    public BrowserType Key => BrowserType.Chrome;
    public Task<IWebDriver> Create(WebDriverOptions sessionOptions)
    {
        var driverService = ChromeDriverService.CreateDefaultService();
        ChromeOptions chromeOptions = new();
        chromeOptions.AddArguments(sessionOptions.Browser.CustomOptions);
        if (!sessionOptions.Browser.ShowBrowser)
        {
            // see https://www.selenium.dev/blog/2023/headless-is-going-away/
            chromeOptions.AddArgument("--headless=new");
        }
        return Task.FromResult((IWebDriver)new ChromeDriver(driverService, chromeOptions));
    }
}
