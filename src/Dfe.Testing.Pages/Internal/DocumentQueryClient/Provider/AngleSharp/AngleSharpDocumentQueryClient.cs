namespace Dfe.Testing.Pages.Internal.DocumentQueryClient.Provider.AngleSharp;
internal class AngleSharpDocumentQueryClient : IDocumentQueryClient
{
    private readonly IHtmlDocument _htmlDocument;
    public AngleSharpDocumentQueryClient(IHtmlDocument document)
    {
        ArgumentNullException.ThrowIfNull(document);
        _htmlDocument = document;
    }

    public void Run(QueryOptions args, Action<IDocumentPart> handler)
    {
        ArgumentNullException.ThrowIfNull(args);
        ArgumentNullException.ThrowIfNull(args.Query);
        if (args.InScope == null)
        {
            handler(
                AsDocumentPart(
                    QueryForElementInScope(_htmlDocument, args.Query)));
            return;
        }

        var scope = QueryForElementInScope(scope: _htmlDocument, selector: args.InScope);
        handler(
            AsDocumentPart(
                QueryForElementInScope(scope, args.Query)));
    }

    public IDocumentPart Query(QueryOptions queryArgs)
    {
        ArgumentNullException.ThrowIfNull(queryArgs);
        ArgumentNullException.ThrowIfNull(queryArgs.Query);
        var element = queryArgs.InScope == null ?
            QueryForElementInScope(_htmlDocument, queryArgs.Query) :
                // find the scope and query within
                QueryForElementInScope(
                    scope: QueryForElementInScope(scope: _htmlDocument, selector: queryArgs.InScope),
                    selector: queryArgs.Query);

        return AsDocumentPart(element);
    }


    public IEnumerable<IDocumentPart> QueryMany(QueryOptions queryArgs)
    {
        ArgumentNullException.ThrowIfNull(queryArgs);
        ArgumentNullException.ThrowIfNull(queryArgs.Query);
        var elements =
            (queryArgs.InScope == null ?
                QueryForMultipleElementsFromScope(scope: _htmlDocument, selector: queryArgs.Query) :
                    // find the scope and query within
                    QueryForMultipleElementsFromScope(
                        scope: QueryForElementInScope(scope: _htmlDocument, selector: queryArgs.InScope),
                        selector: queryArgs.Query)).ToList();

        return AsDocumentParts(elements);
    }



    private static IElement QueryForElementInScope(IParentNode scope, IElementSelector selector)
    {
        var elements = scope.QuerySelectorAll(selector.ToSelector());
        if (elements == null || elements.Length == 0)
        {
            throw new ArgumentException($"No elements found in scope using selector {selector.ToSelector()}");
        }
        if (elements.Length > 1)
        {
            throw new ArgumentException($"Multiple elements found in scope using selector {selector.ToSelector()}");
        };
        return elements.Single();
    }

    private static IEnumerable<IElement> QueryForMultipleElementsFromScope(IParentNode scope, IElementSelector selector)
    {
        var elements = scope.QuerySelectorAll(selector.ToSelector());
        return elements == null || elements.Length == 0 ? ([]) : elements;
    }


    private static AngleSharpDocumentPart AsDocumentPart(IElement element) => new(element);
    private static IEnumerable<AngleSharpDocumentPart> AsDocumentParts(IEnumerable<IElement> elements) => elements?.Select(AsDocumentPart).ToList() ?? [];

    private sealed class AngleSharpDocumentPart : IDocumentPart
    {
        private readonly IElement _element;

        public AngleSharpDocumentPart(IElement element)
        {
            ArgumentNullException.ThrowIfNull(element);
            _element = element;
        }

        public string Text
        {
            get => _element.TextContent ?? string.Empty;
            set => _element.TextContent = value;
        }

        public string TagName => _element.TagName.ToLowerInvariant();

        public void Click()
        {
            throw new NotImplementedException("Clicking is not available with an AngleSharp client");
        }

        public bool HasAttribute(string attributeName) => GetAttribute(attributeName) != null;

        public string? GetAttribute(string attributeName)
        {
            ArgumentNullException.ThrowIfNull(attributeName);
            return _element.GetAttribute(attributeName);
        }

        public IDictionary<string, string> GetAttributes()
            => _element.Attributes?.ToDictionary(
                    keySelector: (attr) => attr.Name,
                    elementSelector: (attr) => attr.Value) ?? [];

        public IDocumentPart? GetChild(IElementSelector selector)
        {
            ArgumentNullException.ThrowIfNull(selector);
            var child = _element.QuerySelector(selector.ToSelector());
            return null == child ? null : new AngleSharpDocumentPart(child);
        }

        public IEnumerable<IDocumentPart> GetChildren() => AsDocumentParts(_element.Children).ToList();
        public IEnumerable<IDocumentPart> GetChildren(IElementSelector selector)
            => AsDocumentParts(
                _element.QuerySelectorAll(selector?.ToSelector() ?? throw new ArgumentNullException("selector when queryAll children is null")));
    }
}
