namespace Dfe.Testing.Pages.BrowserAdaptor.Contracts.Elements.Find;
public sealed class FindElementRequest
{
    public FindElementRequest(FindElementOptions findOptions)
    {
        ArgumentNullException.ThrowIfNull(findOptions);
        FindOptions = findOptions;
    }

    public FindElementOptions FindOptions { get; }
}
