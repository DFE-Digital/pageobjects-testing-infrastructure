namespace Dfe.Testing.Pages.Internal.WebDriver.Provider.Adaptor;
public interface IWebDriverAdaptor : IApplicationNavigator
{
    Task StartAsync();
    Task StartAsync(Action<WebDriverOptions> configureOptions);
    Cookie? GetCookie(string cookieName);
    IEnumerable<Cookie?> GetCookies();
    Task TakeScreenshotAsync();
    // TODO need to adapt this to hide Selenium and return something mapped -- NOTE will need to be able to Find from the element
    // TODO extend to include FindOptions per request?
    internal IWebElement FindElement(IElementSelector selector);
    internal IReadOnlyCollection<IWebElement> FindElements(IElementSelector selector);
    TOut RunJavascript<TOut>(string script, Func<object, TOut> handler, params object[] scriptArgs);
    //TODO something to mock a request?
}
