using OpenQA.Selenium.Chrome;

namespace Dfe.Testing.Pages.BrowserAdaptor.Contracts.Internal;
internal sealed class ChromeWebDriverFactory : IWebDriverFactory<ChromeDriver>
{
    public async Task<ChromeDriver> CreateDriverAsync(BrowserStartSessionRequest options)
    {
        ChromeOptions chromeOptions = new();
        ChromeDriver chromeDriver = new(chromeOptions);

        if (!options.Browser.EnableNetworkingMonitoring)
        {
            return chromeDriver;
        }

        await chromeDriver.Manage().Network.StartMonitoring();
        return chromeDriver;
    }
}
