namespace Dfe.Testing.Pages.BrowserAdaptor.Contracts;
public class BrowserStartSessionRequest
{
    public ApplicationOptions Application { get; } = new();
    public BrowserOptions Browser { get; } = new();
}
