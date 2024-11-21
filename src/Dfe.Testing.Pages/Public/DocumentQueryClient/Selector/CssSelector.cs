namespace Dfe.Testing.Pages.Public.DocumentQueryClient.Selector;
public sealed class CssSelector : IElementSelector
{
    private readonly string _locator;

    public CssSelector(string locator)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(locator, nameof(locator));
        _locator = locator;
    }

    public string ToSelector() => _locator;
    public override string ToString() => ToSelector();
}