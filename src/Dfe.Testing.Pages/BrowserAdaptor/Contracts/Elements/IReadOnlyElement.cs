namespace Dfe.Testing.Pages.BrowserAdaptor.Contracts.Elements;
public interface IReadOnlyElement
{
    bool Displayed { get; }
    bool Selected { get; }
    string Text { get; }
    IEnumerable<string> GetAttributeValuesByName(string attributeName);
}
