namespace Dfe.Testing.Pages.BrowserAdaptor.Contracts;
public sealed class ApplicationOptions
{
    private const int DefaultHttpsPort = 443;
    public string SchemeProtocol { get; set; } = "https";
    public string Domain { get; set; } = "localhost";
    public int Port { get; set; } = DefaultHttpsPort;
    public string Uri => $"{SchemeProtocol}://{Domain.TrimEnd('/')}{PortAsUrl}";
    private string PortAsUrl => Port == DefaultHttpsPort ? string.Empty : $":{Port}";
}
