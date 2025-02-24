namespace Dfe.Testing.Pages.Public.PageObjects.Selector;

public sealed class IdSelector : IElementSelector
{
    private readonly string _selector;

    public IdSelector(string selector)
    {
        ArgumentNullException.ThrowIfNull(selector, nameof(selector));
        _selector = selector;
    }
    public string ToSelector() => _selector.StartsWith('#') ? _selector : $"#{_selector}";

}
