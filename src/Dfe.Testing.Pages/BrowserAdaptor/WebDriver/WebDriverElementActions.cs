using Dfe.Testing.Pages.BrowserAdaptor.Contracts.Elements;
using Dfe.Testing.Pages.BrowserAdaptor.Contracts.Elements.Find;
using Dfe.Testing.Pages.BrowserAdaptor.Contracts.Elements.SendKeys;

namespace Dfe.Testing.Pages.BrowserAdaptor.WebDriver;
internal sealed class WebDriverElementActions : IElementActions
{
    private readonly IWebDriver _webDriver;

    public WebDriverElementActions(IWebDriver webDriver)
    {
        ArgumentNullException.ThrowIfNull(webDriver);
        _webDriver = webDriver;
    }

    public IEnumerable<IReadOnlyElement> Find(FindElementRequest request)
    {
        var elements =
            FindElements(request.FindOptions)
            .Select(
                (element) => request.FindOptions.ElementAttributeEvaluateMode switch
                {
                    ElementAttributeEvaluationMode.Eager => new WebDriverReadOnlyElementEagerLoad(_webDriver, element),
                    _ => throw new NotImplementedException()
                })
                .ToList();

        return elements;
    }

    public void SendKeysTo(SendKeysToElementRequest request)
    {
        var element = FindElements(request.FindOptions).Single();
        request.GetKeysToSend().ToList().ForEach(element.SendKeys);
    }

    public void Click()
    {
        throw new NotImplementedException();
    }

    private IEnumerable<IWebElement> FindElements(FindElementOptions options)
    {
        if (options.InScope == null)
        {
            return _webDriver.FindElements(
                By.CssSelector(options.FindWithSelector));
        }

        IWebElement? scope =
            _webDriver.FindElement(
                By.CssSelector(options.InScope));

        if (scope is null)
        {
            throw new ArgumentNullException($"Unable to find scoped element with {options.InScope}");
        }

        return scope.FindElements(
            By.CssSelector(options.FindWithSelector));
    }
}
