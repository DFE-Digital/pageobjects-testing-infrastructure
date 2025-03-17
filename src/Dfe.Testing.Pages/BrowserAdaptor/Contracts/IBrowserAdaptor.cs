using Dfe.Testing.Pages.BrowserAdaptor.Contracts.Navigate;
using Dfe.Testing.Pages.BrowserAdaptor.Contracts.Network;
using INavigation = Dfe.Testing.Pages.BrowserAdaptor.Contracts.Navigate.INavigation;

namespace Dfe.Testing.Pages.BrowserAdaptor.Contracts;
public interface IBrowserAdaptor
{
    Task<IBrowser> StartSessionAsync(BrowserStartSessionRequest? options);
    Task<IBrowser> StartSessionAsync(Action<BrowserStartSessionRequest> configureOptions);
}
