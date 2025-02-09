using Dfe.Testing.Pages.Internal.DocumentClient.Provider.GetTextHandler;

namespace Dfe.Testing.Pages.Internal.DocumentClient.Provider.AngleSharp;

internal class AngleSharpDocumentClient : IDocumentClient
{
    private readonly IHtmlDocument _htmlDocument;
    private readonly IGetTextProcessingHandler _getTextProcessingHandler;
    public AngleSharpDocumentClient(
        IGetTextProcessingHandler getTextProcessingHandler,
        IHtmlDocument document)
    {
        ArgumentNullException.ThrowIfNull(document);
        ArgumentNullException.ThrowIfNull(getTextProcessingHandler);
        _htmlDocument = document;
        _getTextProcessingHandler = getTextProcessingHandler;
    }

    public void Run(FindOptions args, Action<IDocumentSection> handler)
    {
        ArgumentNullException.ThrowIfNull(args);
        ArgumentNullException.ThrowIfNull(args.Find);

        if (args.InScope == null)
        {
            IDocumentSection documentSection = AsDocumentPart(
                QueryForElementInScope(_htmlDocument, args.Find),
                _getTextProcessingHandler);

            handler(documentSection);
            return;
        }

        var scopedDocumentSection = QueryForElementInScope(scope: _htmlDocument, selector: args.InScope);
        IDocumentSection targetedSectionFromScope =
            AsDocumentPart(
                QueryForElementInScope(scopedDocumentSection, args.Find),
                _getTextProcessingHandler);

        handler(targetedSectionFromScope);
    }

    public IDocumentSection Query(FindOptions queryArgs)
    {
        ArgumentNullException.ThrowIfNull(queryArgs);
        ArgumentNullException.ThrowIfNull(queryArgs.Find);
        var element = queryArgs.InScope == null ?
            QueryForElementInScope(_htmlDocument, queryArgs.Find) :
                // find the scope and query within
                QueryForElementInScope(
                    scope: QueryForElementInScope(_htmlDocument, queryArgs.InScope),
                    selector: queryArgs.Find);

        return AsDocumentPart(
                element,
                _getTextProcessingHandler);
    }

    public IEnumerable<IDocumentSection> QueryMany(FindOptions queryArgs)
    {
        ArgumentNullException.ThrowIfNull(queryArgs);
        ArgumentNullException.ThrowIfNull(queryArgs.Find);
        var elements =
            (queryArgs.InScope == null ?
                QueryForMultipleElementsFromScope(scope: _htmlDocument, selector: queryArgs.Find) :
                    // find the scope and query within
                    QueryForMultipleElementsFromScope(
                        scope: QueryForElementInScope(scope: _htmlDocument, selector: queryArgs.InScope),
                        selector: queryArgs.Find)).ToList();

        return AsDocumentParts(
                elements,
                _getTextProcessingHandler);
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


    private static AngleSharpDocumentSection AsDocumentPart(
        IElement element,
        IGetTextProcessingHandler textQueryHandler) => new(element, textQueryHandler);

    private static IEnumerable<AngleSharpDocumentSection> AsDocumentParts(
        IEnumerable<IElement> elements,
        IGetTextProcessingHandler textProcessingStrategy)
            => elements?.Select((documentSection) => AsDocumentPart(documentSection, textProcessingStrategy))
                    .ToList() ?? [];

    private sealed class AngleSharpDocumentSection : IDocumentSection
    {
        private readonly IElement _wrappedElement;
        private readonly IGetTextProcessingHandler _getTextProcessingHandler;

        public AngleSharpDocumentSection(
            IElement wrappedElement,
            IGetTextProcessingHandler textProcessingStrategy)
        {
            ArgumentNullException.ThrowIfNull(wrappedElement);
            _wrappedElement = wrappedElement;
            _getTextProcessingHandler = textProcessingStrategy;
        }

        public string Text
        {
            get => _getTextProcessingHandler.Handle(_wrappedElement.Text());
            set => _wrappedElement.TextContent = value;
        }

        public string TagName => _wrappedElement.TagName.ToLowerInvariant();

        public string Document => _wrappedElement.ToHtml();

        public void Click()
        {
            throw new NotImplementedException("Clicking is not available with an AngleSharp client");
        }

        public bool HasAttribute(string attributeName) => GetAttribute(attributeName) != null;

        public string? GetAttribute(string attributeName)
        {
            ArgumentNullException.ThrowIfNull(attributeName);
            return _wrappedElement.GetAttribute(attributeName);
        }

        public IEnumerable<IDocumentSection> GetChildren()
            => AsDocumentParts(_wrappedElement.Children, _getTextProcessingHandler)
                .ToList();

        public IDocumentSection? FindDescendant(IElementSelector selector)
        {
            ArgumentNullException.ThrowIfNull(selector);
            var child = _wrappedElement.QuerySelector(selector.ToSelector());
            return child == null ? null : new AngleSharpDocumentSection(child, _getTextProcessingHandler);
        }

        public IEnumerable<IDocumentSection> FindDescendants(IElementSelector selector)
            => AsDocumentParts(
                _wrappedElement.QuerySelectorAll(selector?.ToSelector() ?? throw new ArgumentNullException("selector when queryAll children is null")),
                _getTextProcessingHandler);
        public override string ToString() => Document;
    }

}
