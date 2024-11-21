namespace Dfe.Testing.Pages.Public.DocumentQueryClient.Selector.XPath;

public sealed class ChildXPathSelector : IElementSelector
{
    private const string ChildrenXpathPrefix = ".//";
    private readonly string _xpath;

    public ChildXPathSelector(string? selector = null)
    {
        _xpath = string.IsNullOrEmpty(selector) ? "*" : selector;
    }

    public string ToSelector()
        => _xpath.StartsWith(ChildrenXpathPrefix) ?
            _xpath :
            $"{ChildrenXpathPrefix}{_xpath}";
}
