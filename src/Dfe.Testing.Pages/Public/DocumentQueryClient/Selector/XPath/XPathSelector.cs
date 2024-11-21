namespace Dfe.Testing.Pages.Public.DocumentQueryClient.Selector.XPath;

public sealed class XPathSelector : IElementSelector
{
    private const string GlobalXPathPrefix = "//";
    private readonly string _xpath;

    public XPathSelector(string selector)
    {
        _xpath = string.IsNullOrEmpty(selector) ? string.Empty : selector;
    }

    public string ToSelector()
        => _xpath.StartsWith(GlobalXPathPrefix) ?
            _xpath :
            $"{GlobalXPathPrefix}{_xpath}";
}
