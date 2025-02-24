using Dfe.Testing.Pages.Contracts.Selector;

namespace Dfe.Testing.Pages.Contracts.Documents;
public interface IDocumentSection
{
    string Document { get; }
    string Text { get; set; }
    string TagName { get; }
    IEnumerable<KeyValuePair<string, string?>> Attributes { get; }
    bool HasAttribute(string attributeName);
    string? GetAttribute(string attributeName);
    IEnumerable<IDocumentSection> GetChildren();
    IDocumentSection? FindDescendant(IElementSelector selector);
    IEnumerable<IDocumentSection> FindDescendants(IElementSelector selector);
    void Click();
}
