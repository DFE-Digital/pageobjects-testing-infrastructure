using Dfe.Testing.Pages.Contracts.Documents;
using Dfe.Testing.Pages.Contracts.Selector.XPath;
using Dfe.Testing.Pages.Internal.DocumentClient.Provider.GetTextHandler;

namespace Dfe.Testing.Pages.Internal.DocumentClient.Provider.WebDriver;

internal sealed class WebDriverDocumentClient : IDocumentClient
{
    private readonly IWebDriverAdaptor _webDriverAdaptor;
    private readonly IGetTextProcessingHandler _textProcessingHandler;

    public WebDriverDocumentClient(
        IWebDriverAdaptor webDriverAdaptor,
        IGetTextProcessingHandler textProcessingHandler)
    {
        ArgumentNullException.ThrowIfNull(webDriverAdaptor);
        _webDriverAdaptor = webDriverAdaptor;
        _textProcessingHandler = textProcessingHandler;
    }

    public void Run(FindOptions args, Action<IDocumentSection> handler) => handler(Query(args));

    public IDocumentSection Query(FindOptions args)
    {
        if (args.InScope == null)
        {
            var results = QueryManyGlobal(args);

            return results.Count == 0
                ? throw new ArgumentException($"element not found with {args.Find!.ToSelector()}")
                : results.Count > 1
                ? throw new ArgumentException($"multiple elements found with query {args.Find}, cannot query for a single element")
                : results.Single();
        }

        var scope = QueryManyInScope(args);

        return scope.Count == 0
            ? throw new ArgumentException($"element scope not found with {args.InScope!.ToSelector()}")
            : scope.Count > 1
            ? throw new ArgumentException($"multiple elements found with in scope {args.InScope.ToSelector()}, cannot query for a single element")
            : scope.Single().FindDescendant(args.Find!) ??
            throw new ArgumentNullException($"element {args.Find!.ToSelector()} not found in scope {args.InScope!.ToSelector()}");
    }

    public IEnumerable<IDocumentSection> QueryMany(FindOptions queryArgs)
        => queryArgs.InScope == null ? QueryManyGlobal(queryArgs) : QueryManyInScope(queryArgs);


    // TODO expression is evaluated immediately on IEnumerable<T> because otherwise stale references to elements that have changed...
    // Could improve design by capturing queryscope inside of DocumentPart so it's retryable? Come back to
    private List<IDocumentSection> QueryManyInScope(FindOptions queryArgs)
    {
        ValidateQueryOptions(queryArgs);

        // find scope then query in scope
        var target =
            _webDriverAdaptor.FindElement(queryArgs.InScope!)
                .FindElements(
                    WebDriverByLocatorHelpers.CreateLocator(queryArgs.Find!));

        return CreateDocumentSection(
                _webDriverAdaptor,
                target,
                _textProcessingHandler)
            .ToList();
    }

    private List<IDocumentSection> QueryManyGlobal(FindOptions args)
    {
        ValidateQueryOptions(args);

        return CreateDocumentSection(
                _webDriverAdaptor,
                _webDriverAdaptor.FindElements(args.Find!),
                _textProcessingHandler)
            .ToList();
    }

    private static void ValidateQueryOptions(FindOptions args)
    {
        ArgumentNullException.ThrowIfNull(args);
        ArgumentNullException.ThrowIfNull(args.Find);
    }

    private static IDocumentSection CreateDocumentSection(
        IWebDriverAdaptor adaptor,
        IWebElement element,
        IGetTextProcessingHandler handler) => new WebDriverDocumentPart(adaptor, element, handler);

    private static IEnumerable<IDocumentSection> CreateDocumentSection(IWebDriverAdaptor adaptor, IEnumerable<IWebElement> elements, IGetTextProcessingHandler handler)
        => elements?.Select((section)
            => CreateDocumentSection(adaptor, section, handler)) ?? [];

    private sealed class WebDriverDocumentPart : IDocumentSection
    {
        private readonly IWebDriverAdaptor _adaptor;
        private readonly IWebElement _wrappedElement;
        private readonly IGetTextProcessingHandler _textHandler;

        public WebDriverDocumentPart(
            IWebDriverAdaptor adaptor,
            IWebElement element,
            IGetTextProcessingHandler textHandler)
        {
            ArgumentNullException.ThrowIfNull(element);
            ArgumentNullException.ThrowIfNull(textHandler);
            _adaptor = adaptor;
            _wrappedElement = element;
            _textHandler = textHandler;
        }

        public string Text
        {
            get => _textHandler.Handle(_wrappedElement.Text);
            set => _wrappedElement.SendKeys(value);
        }
        public string TagName => _wrappedElement.TagName;
        public string Document => _wrappedElement.GetAttribute("outerHTML"); // innerHTML for internal if needed

        // WebDriver does not provide a way to access elements through the WebElement API or WebDriver API.
        // Options are
        // - pass in every known attribute; incurring a WebDriver network call each time
        // - execute JavaScript to get all attributes
        public IEnumerable<KeyValuePair<string, string?>> Attributes
        {
            get
            {
                string script =
                    """
                    var elementAttributes = arguments[0].attributes;
                    var items = {};

                    for (index = 0; index < elementAttributes.length; ++index)
                    {
                        var element = elementAttributes[index];
                        items[element.name] = element.value;
                    };

                    return items;
                    """;

                return _adaptor.RunJavascript(script, attributeHandler, _wrappedElement);

                static Dictionary<string, string?> attributeHandler(object scriptOutput)
                {
                    var attributesDictionary = new Dictionary<string, string?>();
                    if (scriptOutput is IDictionary<string, object> attributesObject)
                    {
                        foreach (var attribute in attributesObject)
                        {
                            attributesDictionary.Add(attribute.Key, attribute.Value.ToString());
                        }
                    }
                    return attributesDictionary;
                }
            }
        }

        public bool HasAttribute(string attributeName) => GetAttribute(attributeName) != null;
        public string? GetAttribute(string attributeName)
        {
            ArgumentException.ThrowIfNullOrEmpty(attributeName);
            return _wrappedElement.GetAttribute(attributeName);
        }

        public IEnumerable<IDocumentSection> GetChildren()
            => CreateDocumentSection(
                    _adaptor, FindMany(WebDriverByLocatorHelpers.AsXPath(new ChildXPathSelector())), _textHandler)
                .ToList();

        public IDocumentSection? FindDescendant(IElementSelector selector) => FindDocumentPart(selector);

        public IEnumerable<IDocumentSection> FindDescendants(IElementSelector selector)
            => FindMany(
                    WebDriverByLocatorHelpers.CreateLocator(selector))
                .Select(section => CreateDocumentSection(_adaptor, section, _textHandler));

        private IDocumentSection FindDocumentPart(IElementSelector selector)
            // TODO pass in an error message into the collection extensions?
            => CreateDocumentSection(
                    _adaptor,
                    FindMany(WebDriverByLocatorHelpers.CreateLocator(selector)).ThrowIfNullOrEmpty().ThrowIfMultiple(),
                    _textHandler)
                .Single();

        private ReadOnlyCollection<IWebElement> FindMany(By by) => _wrappedElement.FindElements(by) ?? Array.Empty<IWebElement>().AsReadOnly();

        public void Click() => _wrappedElement.Click();
        public override string ToString() => Document;
    }
}
