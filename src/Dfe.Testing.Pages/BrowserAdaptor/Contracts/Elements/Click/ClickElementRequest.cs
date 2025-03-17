using Dfe.Testing.Pages.BrowserAdaptor.Contracts.Elements.Find;

namespace Dfe.Testing.Pages.BrowserAdaptor.Contracts.Elements.Click;
public sealed class ClickElementRequest// Seperate action for PointOptions (x, y)
{
    private ClickElementRequest(FindElementOptions findOptions)
    {
        ArgumentNullException.ThrowIfNull(findOptions);
        FindOptions = findOptions;
    }

    public static ClickElementRequest Create(FindElementOptions findOptions) => new(findOptions);
    public FindElementOptions FindOptions { get; }
}
