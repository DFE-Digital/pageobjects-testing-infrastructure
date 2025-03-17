namespace Dfe.Testing.Pages.BrowserAdaptor.Contracts.Elements;
public interface IReadOnlyElement
{
    string Text { get; }
    IEnumerable<string> GetAttributeValuesByName(string attributeName);
}
