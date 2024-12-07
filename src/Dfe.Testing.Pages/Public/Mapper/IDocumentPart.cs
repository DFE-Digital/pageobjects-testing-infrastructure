namespace Dfe.Testing.Pages.Public.Mapper;
public interface IDocumentPart
{
    string Text { get; set; }
    string TagName { get; }
    bool HasAttribute(string attributeName);
    string? GetAttribute(string attributeName);
    IDictionary<string, string> GetAttributes();
    IEnumerable<IDocumentPart> GetChildren();
    IDocumentPart? FindDescendant(IElementSelector selector);
    IEnumerable<IDocumentPart> FindDescendants(IElementSelector selector);
    void Click();
}
