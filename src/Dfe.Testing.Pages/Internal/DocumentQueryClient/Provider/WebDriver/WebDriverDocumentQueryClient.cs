namespace Dfe.Testing.Pages.Internal.DocumentQueryClient.Provider.WebDriver;

internal sealed class WebDriverDocumentQueryClient : IDocumentQueryClient
{
    private readonly IWebDriverAdaptor _webDriverAdaptor;

    public WebDriverDocumentQueryClient(IWebDriverAdaptor webDriverAdaptor)
    {
        ArgumentNullException.ThrowIfNull(webDriverAdaptor);
        _webDriverAdaptor = webDriverAdaptor;
    }

    public void Run(QueryOptions args, Action<IDocumentPart> handler) => handler(Query(args));

    public IDocumentPart Query(QueryOptions args)
    {
        if (args.InScope == null)
        {
            List<IDocumentPart> results = QueryManyGlobal(args);

            return results.Count == 0
                ? throw new ArgumentException($"element not found with {args.Query!.ToSelector()}")
                : results.Count > 1
                ? throw new ArgumentException($"multiple elements found with query {args.Query}, cannot query for a single element")
                : results.Single();
        }

        List<IDocumentPart> scope = QueryManyWithScope(args);

        return scope.Count == 0
            ? throw new ArgumentException($"element scope not found with {args.InScope!.ToSelector()}")
            : scope.Count > 1
            ? throw new ArgumentException($"multiple elements found with in scope {args.InScope.ToSelector()}, cannot query for a single element")
            : scope.Single().FindDescendant(args.Query!) ??
            throw new ArgumentNullException($"element {args.Query!.ToSelector()} not found in scope {args.InScope!.ToSelector()}");
    }

    public IEnumerable<IDocumentPart> QueryMany(QueryOptions queryArgs)
        => queryArgs.InScope == null ? QueryManyGlobal(queryArgs) : QueryManyWithScope(queryArgs);


    // TODO expression is evaluated immediately on IEnumerable<T> because otherwise stale references to elements that have changed...
    // Could improve design by capturing queryscope inside of DocumentPart so it's retryable? Come back to
    private List<IDocumentPart> QueryManyWithScope(QueryOptions queryArgs)
    {
        ValidateQueryOptions(queryArgs);

        return ToDocumentParts(
                _webDriverAdaptor.FindElement(queryArgs.InScope!)
                    .FindElements(WebDriverByLocatorHelpers.CreateLocator(queryArgs.Query!))
                ).ToList();
    }

    private List<IDocumentPart> QueryManyGlobal(QueryOptions args)
    {
        ValidateQueryOptions(args);

        return ToDocumentParts(_webDriverAdaptor.FindElements(args.Query!))
                    .ToList();
    }

    private static void ValidateQueryOptions(QueryOptions args)
    {
        ArgumentNullException.ThrowIfNull(args);
        ArgumentNullException.ThrowIfNull(args.Query);
    }

    private static IDocumentPart ToDocumentParts(IWebElement element) => new WebDriverDocumentPart(element);

    private static IEnumerable<IDocumentPart> ToDocumentParts(IEnumerable<IWebElement> elements) => elements?.Select(ToDocumentParts) ?? [];

    private sealed class WebDriverDocumentPart : IDocumentPart
    {
        private readonly IWebElement _wrappedElement;

        public WebDriverDocumentPart(IWebElement element)
        {
            ArgumentNullException.ThrowIfNull(element);
            _wrappedElement = element;
        }

        public string Text
        {
            get => _wrappedElement.Text ?? string.Empty;
            set => _wrappedElement.SendKeys(value);
        }

        public string TagName => _wrappedElement.TagName;

        public bool HasAttribute(string attributeName) => GetAttribute(attributeName) != null;
        public string? GetAttribute(string attributeName)
        {
            ArgumentException.ThrowIfNullOrEmpty(attributeName);
            return _wrappedElement.GetAttribute(attributeName);
        }

        public IDictionary<string, string> GetAttributes() => throw new NotImplementedException("TODO GetAttributes in WebDriver - parsing over the top or JS");

        public IEnumerable<IDocumentPart> GetChildren()
            => ToDocumentParts(
                FindMany(
                    WebDriverByLocatorHelpers.AsXPath(new ChildXPathSelector())))
                .ToList<IDocumentPart>();

        public IDocumentPart? FindDescendant(IElementSelector selector) => FindDocumentPart(selector);

        public IEnumerable<IDocumentPart> FindDescendants(IElementSelector selector)
            => FindMany(
                    WebDriverByLocatorHelpers.CreateLocator(selector))
                .Select(ToDocumentParts);

        private IDocumentPart FindDocumentPart(IElementSelector selector)
            // TODO pass in an error message into the collection extensions?
            => ToDocumentParts(
                FindMany(
                    WebDriverByLocatorHelpers.CreateLocator(selector))
                .ThrowIfNullOrEmpty()
                .ThrowIfMultiple())
                .Single();

        private ReadOnlyCollection<IWebElement> FindMany(By by) => _wrappedElement.FindElements(by) ?? Array.Empty<IWebElement>().AsReadOnly();

        public void Click() => _wrappedElement.Click();
    }
}
