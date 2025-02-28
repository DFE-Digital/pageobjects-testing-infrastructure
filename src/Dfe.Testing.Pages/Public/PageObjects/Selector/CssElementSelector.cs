﻿namespace Dfe.Testing.Pages.Public.PageObjects.Selector;
public sealed class CssElementSelector : IElementSelector
{
    private readonly string _locator;

    public CssElementSelector(string locator)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(locator, nameof(locator));
        _locator = locator;
    }

    public string ToSelector() => _locator;
    public override string ToString() => ToSelector();
}