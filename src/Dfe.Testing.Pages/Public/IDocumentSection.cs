namespace Dfe.Testing.Pages.Public;
public interface IDocumentSection
{
    string Text { get; set; }
    string TagName { get; }
    bool HasAttribute(string attributeName);
    string? GetAttribute(string attributeName);
    IEnumerable<IDocumentSection> GetChildren();
    IDocumentSection? FindDescendant(IElementSelector selector);
    IEnumerable<IDocumentSection> FindDescendants(IElementSelector selector);
    void Click();
}
