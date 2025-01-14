
namespace Dfe.Testing.Pages.Internal.WebDriver.SessionOptions;
internal interface IWebDriverSessionOptionsBuilder
{
    IWebDriverSessionOptionsBuilder WithBrowserType(string browserType);
    IWebDriverSessionOptionsBuilder WithPageLoadTimeout(int pageLoadTimeoutSeconds);
    IWebDriverSessionOptionsBuilder WithRequestTimeout(int requestTimeoutSeconds);
    IWebDriverSessionOptionsBuilder WithNetworkInterception(bool enable);
    IWebDriverSessionOptionsBuilder WithBrowserOption(params string[] customOption);
    WebDriverSessionOptions Build();
}
