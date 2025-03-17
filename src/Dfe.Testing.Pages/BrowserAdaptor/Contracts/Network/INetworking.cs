namespace Dfe.Testing.Pages.BrowserAdaptor.Contracts.Network;
public interface INetworking
{
    Task StartAsync();
    void AddAuthenticationCredentials(IAuthenticationCredentials credentials);
}

