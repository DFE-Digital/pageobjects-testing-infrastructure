using Dfe.Testing.Pages.BrowserAdaptor.Contracts.Network;

namespace Dfe.Testing.Pages.BrowserAdaptor.Contracts;
public interface IBrowser
{
    INetworking Network { get; }
    Contracts.Navigate.INavigation Navigate { get; }
}
