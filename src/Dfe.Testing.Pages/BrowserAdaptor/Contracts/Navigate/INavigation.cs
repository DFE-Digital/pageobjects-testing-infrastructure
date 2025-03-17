namespace Dfe.Testing.Pages.BrowserAdaptor.Contracts.Navigate;
public interface INavigation
{
    Uri CurrentUri { get; }
    Task NavigateToUriAsync(string uri, CancellationToken ctx = default);
    Task NavigateToUriAsync(Uri uri, CancellationToken ctx = default);
}
