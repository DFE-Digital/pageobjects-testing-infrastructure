namespace Dfe.Testing.Pages.BrowserAdaptor.Contracts.Network;
public sealed class BasicAuthenticationCredentials : IAuthenticationCredentials
{
    public BasicAuthenticationCredentials(string? username, string? password)
    {
        Username = username ?? string.Empty;
        Password = password ?? string.Empty;
    }

    public string Username { get; }
    public string Password { get; }

    public string ToAuthorisationHeader()
    {
        var authorisationHeaderBytes = Encoding.UTF8.GetBytes($"{Username}:{Password}");
        return Convert.ToBase64String(authorisationHeaderBytes);
    }

    public override string ToString() => ToAuthorisationHeader();

}
