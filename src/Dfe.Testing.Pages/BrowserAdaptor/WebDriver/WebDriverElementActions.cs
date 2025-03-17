using Dfe.Testing.Pages.BrowserAdaptor.Contracts.Elements;
using Dfe.Testing.Pages.BrowserAdaptor.Contracts.Elements.Click;
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

    public bool TryFind(FindElementRequest request, out IEnumerable<IReadOnlyElement> elements)
    {
        IEnumerable<IWebElement> webElements = FindInternal(_webDriver, request.FindOptions);
        elements = MapToReadOnlyElements(_webDriver, webElements, request.FindOptions);
        return elements.Any();
    }

    public IEnumerable<IReadOnlyElement> Find(FindElementRequest request)
    {
        IEnumerable<IWebElement> elements = FindInternal(_webDriver, request.FindOptions).ToList();
        return MapToReadOnlyElements(_webDriver, elements, request.FindOptions);
    }

    public void UpdateElement(UpdateElementRequest request)
    {
        IWebElement element = RequireFind(_webDriver, request.FindOptions).Single();

        if (request.Clear)
        {
            // TODO only covers text inputs - extend to other Element types (clearing a radio, checkboxes, select)
            element.SendKeys(Keys.Control + 'a');
            element.SendKeys(Keys.Clear);
        }

        request.KeysToSend
            .ToList()
            .ForEach(element.SendKeys);
    }

    public void Click(ClickElementRequest request)
    {
        IWebElement element = RequireFind(_webDriver, request.FindOptions).Single();
        element.Click();
    }

    private static IEnumerable<IWebElement> RequireFind(IWebDriver driver, FindElementOptions options)
    {
        IEnumerable<IWebElement> elements = FindInternal(driver, options);
        if (elements is null || !elements.Any())
        {
            throw new ArgumentNullException($"Unable to find element with scope: {options.InScope} and selector: {options.FindWithSelector}");
        }
        return elements;
    }

    private static IEnumerable<IWebElement> FindInternal(IWebDriver driver, FindElementOptions options)
    {
        if (options.InScope == null)
        {
            return driver.FindElements(
                By.CssSelector(options.FindWithSelector));
        }

        IWebElement? scope =
            driver.FindElement(
                By.CssSelector(options.InScope));

        return scope switch
        {
            null => [],
            _ => scope.FindElements(
                                By.CssSelector(options.FindWithSelector)),
        };
    }

    private static IEnumerable<IReadOnlyElement> MapToReadOnlyElements(IWebDriver webDriver, IEnumerable<IWebElement> elements, FindElementOptions options)
    {
        return elements?.Select(
                (element) => options.ElementAttributeEvaluateMode switch
                {
                    ElementAttributeEvaluationMode.Eager => new WebDriverReadOnlyElementEagerLoad(webDriver, element),
                    _ => throw new NotImplementedException()
                }) ?? [];
    }
}
