namespace Dfe.Testing.Pages.Internal.WebDriver.Provider.Adaptor;
public interface IApplicationNavigator
{
    Task NavigateToAsync(Uri uri);
    Task BackAsync();
    Task ReloadAsync();
}