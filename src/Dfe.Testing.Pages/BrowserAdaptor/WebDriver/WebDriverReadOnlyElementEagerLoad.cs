using Dfe.Testing.Pages.BrowserAdaptor.Contracts.Elements;
using OpenQA.Selenium.Support.Extensions;

namespace Dfe.Testing.Pages.BrowserAdaptor.WebDriver;
internal sealed class WebDriverReadOnlyElementEagerLoad : IReadOnlyElement
{
    private readonly IWebDriver _webDriver;
    private readonly IWebElement _webElement;
    private readonly IDictionary<string, IEnumerable<string>> _attributes;

    public WebDriverReadOnlyElementEagerLoad(
        IWebDriver webDriver,
        IWebElement webElement)
    {
        ArgumentNullException.ThrowIfNull(webDriver);
        ArgumentNullException.ThrowIfNull(webElement);
        _webDriver = webDriver;
        _webElement = webElement;
        Text = _webElement.Text;
        _attributes = GetAttributes(webElement);
    }

    public string Text { get; }

    public IEnumerable<string> GetAttributeValuesByName(string attributeName)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(attributeName, nameof(attributeName));
        _attributes.TryGetValue(attributeName, out var values);
        return values ?? [];
    }

    private Dictionary<string, IEnumerable<string>> GetAttributes(IWebElement element)
    {
        string getAttributesOfElementAsJavascriptObject =
            """
            var elementAttibutes = arguments[0].attributes;
            var items = {};


            for (var index = 0; index < elementAttibutes.length; index++) {
                var element = elementAttibutes[index];
                items[element.name] = element.value.split(' ').filter(t => t.trim().length > 0);
            }

            return items;
            """;

        Dictionary<string, object> jsExecutionResult =
            _webDriver.ExecuteJavaScript<Dictionary<string, object>>(
                getAttributesOfElementAsJavascriptObject,
                element) ?? [];

        Dictionary<string, IEnumerable<string>> results =
            jsExecutionResult.Select(
                (t) =>
                    (t.Key,
                    (t.Value as IEnumerable<object> ?? []).Select(attributeValue => attributeValue.ToString() ?? string.Empty)))
                        .ToDictionary<(string name, IEnumerable<string> values), string, IEnumerable<string>>(
                            (t) => t.name,
                            (t) => t.values);

        return results ?? [];
    }
}
