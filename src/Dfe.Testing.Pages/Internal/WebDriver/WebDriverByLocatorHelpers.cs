using Dfe.Testing.Pages.Internal.DocumentQueryClient.Selector.Extensions;

namespace Dfe.Testing.Pages.Internal.WebDriver;

internal static class WebDriverByLocatorHelpers
{
    internal static By AsXPath(IElementSelector selector) => By.XPath(selector.ToSelector());
    internal static By AsCssSelector(IElementSelector selector) => By.CssSelector(selector.ToSelector());
    internal static By CreateLocator(IElementSelector selector)
        => selector.IsSelectorXPathConvention() ?
            AsXPath(selector) :
            AsCssSelector(selector);
}
