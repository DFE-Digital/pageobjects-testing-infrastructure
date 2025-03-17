using Dfe.Testing.Pages.BrowserAdaptor.Contracts.Elements.Find;

namespace Dfe.Testing.Pages.BrowserAdaptor.Contracts.Elements.SendKeys;
public sealed class UpdateElementRequest
{
    private UpdateElementRequest(FindElementOptions options)
    {
        ArgumentNullException.ThrowIfNull(options);
        FindOptions = options;
    }

    public static UpdateElementRequest Create(FindElementOptions options) => new(options);
    public List<string> KeysToSend { get; } = [];
    public bool Clear { get; set; } = false;
    public FindElementOptions FindOptions { get; }
}
