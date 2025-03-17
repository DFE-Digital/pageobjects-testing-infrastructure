using Dfe.Testing.Pages.Public.PageObjects;
using OpenQA.Selenium.Chrome;

namespace Dfe.Testing.Pages.BrowserAdaptor.Contracts.Internal;
internal sealed class ChromeWebDriverFactory : IWebDriverFactory<ChromeDriver>
{
    private readonly IMapper<BrowserStartSessionRequest, ChromeOptions> _optionsMapper;

    public ChromeWebDriverFactory()
    {
        _optionsMapper = BrowserSessionOptionsToChromeOptionsMapper.Default;
    }
    public async Task<ChromeDriver> CreateDriverAsync(BrowserStartSessionRequest options)
    {
        ChromeOptions chromeOptions = _optionsMapper.Map(options);
        ChromeDriver chromeDriver = new(chromeOptions);

        if (!options.Browser.EnableNetworkingMonitoring)
        {
            return chromeDriver;
        }

        await chromeDriver.Manage().Network.StartMonitoring();
        return chromeDriver;
    }
}

internal sealed class BrowserSessionOptionsToChromeOptionsMapper : IMapper<BrowserStartSessionRequest, ChromeOptions>
{
    public ChromeOptions Map(BrowserStartSessionRequest input)
    {
        ChromeOptions chromeOptions = new()
        {
            PageLoadTimeout = TimeSpan.FromSeconds(input.Browser.PageTimeoutSeconds),
            BrowserVersion = input.Browser.BrowserVersion?.ToString() ?? string.Empty
        };
        chromeOptions.AddArguments(input.Browser.DriverFlags);
        return chromeOptions;
    }

    public static IMapper<BrowserStartSessionRequest, ChromeOptions> Default => new BrowserSessionOptionsToChromeOptionsMapper();
}
