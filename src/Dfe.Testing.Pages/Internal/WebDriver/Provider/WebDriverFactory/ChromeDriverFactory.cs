using OpenQA.Selenium.Chrome;

namespace Dfe.Testing.Pages.Internal.WebDriver.Provider.WebDriverFactory;
internal sealed class ChromeDriverFactory : WebDriverFactoryBase<ChromeDriver>
{
    public override Task<Func<ChromeDriver>> CreateDriver(WebDriverSessionOptions sessionOptions)
    {
        // TODO could enable caching driverService in the abstract class - so that when new ChromeDriver it's created from the cached instance?
        var driverService = ChromeDriverService.CreateDefaultService();
        ChromeOptions options = new();
        options.AddArgument("--headless");
        return Task.FromResult(
            () => new ChromeDriver(driverService, options));
    }
}
