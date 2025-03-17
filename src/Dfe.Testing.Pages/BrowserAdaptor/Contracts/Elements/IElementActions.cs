using Dfe.Testing.Pages.BrowserAdaptor.Contracts.Elements.Find;
using Dfe.Testing.Pages.BrowserAdaptor.Contracts.Elements.SendKeys;

namespace Dfe.Testing.Pages.BrowserAdaptor.Contracts.Elements;
public interface IElementActions
{
    void SendKeysTo(SendKeysToElementRequest request);
    void Click();
    IEnumerable<IReadOnlyElement> Find(FindElementRequest request);
    bool TryFind(FindElementRequest request, out IEnumerable<IReadOnlyElement> elements);
}
