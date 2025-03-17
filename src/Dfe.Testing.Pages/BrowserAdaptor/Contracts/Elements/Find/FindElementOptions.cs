namespace Dfe.Testing.Pages.BrowserAdaptor.Contracts.Elements.Find;
public sealed class FindElementOptions
{
    private FindElementOptions(string selector)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(selector, nameof(selector));
        FindWithSelector = selector;
    }

    public static FindElementOptions Create(string selector) => new(selector);
    public string FindWithSelector { get; }
    public string? InScope { get; set; } = null;
    public ElementAttributeEvaluationMode ElementAttributeEvaluateMode { get; set; } = ElementAttributeEvaluationMode.Eager;
    public int? Limit { get; set; } = default;
    // TODO consider Filter delegate
    // TODO infer from the string the type of selector that is being requested. XPath, CSS 
}
