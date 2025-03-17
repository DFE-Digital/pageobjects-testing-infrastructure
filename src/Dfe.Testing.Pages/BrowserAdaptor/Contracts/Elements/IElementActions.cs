using Dfe.Testing.Pages.BrowserAdaptor.Contracts.Elements.Click;
using Dfe.Testing.Pages.BrowserAdaptor.Contracts.Elements.Find;
using Dfe.Testing.Pages.BrowserAdaptor.Contracts.Elements.SendKeys;

namespace Dfe.Testing.Pages.BrowserAdaptor.Contracts.Elements;
public interface IElementActions
{
    IEnumerable<IReadOnlyElement> Find(FindElementRequest request);
    bool TryFind(FindElementRequest request, out IEnumerable<IReadOnlyElement> elements);
    void UpdateElement(UpdateElementRequest request);
    void Click(ClickElementRequest request);
}
