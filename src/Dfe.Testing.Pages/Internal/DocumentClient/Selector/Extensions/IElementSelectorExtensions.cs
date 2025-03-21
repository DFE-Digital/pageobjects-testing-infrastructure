﻿using Dfe.Testing.Pages.Public.PageObjects.Selector;

namespace Dfe.Testing.Pages.Internal.DocumentClient.Selector.Extensions;

internal static class IElementSelectorExtensions
{
    internal static bool IsSelectorXPathConvention(this IElementSelector selector)
    {
        const string entireDocumentPrefix = "//";
        var selectWith = selector.ToSelector();
        return selectWith.StartsWith(entireDocumentPrefix) || selectWith.StartsWith(".//");
    }
}
