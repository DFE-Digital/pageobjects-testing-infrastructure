namespace Dfe.Testing.Pages.BrowserAdaptor.Contracts;
public interface IBrowserAdaptor
{
    Task<IBrowser> StartSessionAsync(BrowserStartSessionRequest? options = null);
    Task<IBrowser> StartSessionAsync(Action<BrowserStartSessionRequest> configureOptions);
}
