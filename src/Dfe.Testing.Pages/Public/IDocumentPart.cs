﻿namespace Dfe.Testing.Pages.Public;
public interface IDocumentPart
{
    string Text { get; set; }
    string TagName { get; }
    bool HasAttribute(string attributeName);
    string? GetAttribute(string attributeName);
    IDictionary<string, string> GetAttributes();
    IEnumerable<IDocumentPart> GetChildren();
    IEnumerable<IDocumentPart> GetChildren(IElementSelector selector);
    IDocumentPart? GetChild(IElementSelector selector);
    void Click();
}
