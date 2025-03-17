using Dfe.Testing.Pages.BrowserAdaptor.Contracts.Elements.Find;

namespace Dfe.Testing.Pages.BrowserAdaptor.Contracts.Elements.SendKeys;
public sealed class SendKeysToElementRequest
{
    private readonly List<string> _keys = [];

    public SendKeysToElementRequest(FindElementOptions options)
    {
        ArgumentNullException.ThrowIfNull(options);
        FindOptions = options;
    }

    public static SendKeysToElementRequest Create(FindElementOptions options) => new(options);

    public void AddInputKeys(params string[] key)
    {
        ArgumentNullException.ThrowIfNull(key);
        key?.Where(t => !string.IsNullOrEmpty(t))
            .ToList()
            .ForEach(_keys.Add);
    }

    public IEnumerable<string> GetKeysToSend() => _keys;
    public bool Clear { get; set; } = false;
    public FindElementOptions FindOptions { get; }
}
