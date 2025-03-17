using Dfe.Testing.Pages.BrowserAdaptor.Contracts.Network;

namespace Dfe.Testing.Pages.BrowserAdaptor.WebDriver;
internal sealed class WebDriverNetworking : INetworking
{
    private readonly List<NetworkAuthenticationHandler> _handlers = [];
    private readonly INetwork _webDriverNetwork;

    public WebDriverNetworking(INetwork webDriverNetwork)
    {
        ArgumentNullException.ThrowIfNull(webDriverNetwork);
        _webDriverNetwork = webDriverNetwork;
    }

    public void AddAuthenticationCredentials(IAuthenticationCredentials credentials)
    {
        NetworkAuthenticationHandler handler = new NetworkAuthenticationHandler();

        if (credentials is BasicAuthenticationCredentials basicAuthCredentials)
        {
            handler.Credentials = new PasswordCredentials(basicAuthCredentials.Username, basicAuthCredentials.Password);
        }

        _handlers.Add(handler);
    }

    public async Task StartAsync()
    {
        foreach (var handler in _handlers)
        {
            _webDriverNetwork.AddAuthenticationHandler(handler);
        }
        await _webDriverNetwork.StartMonitoring();
    }
}
