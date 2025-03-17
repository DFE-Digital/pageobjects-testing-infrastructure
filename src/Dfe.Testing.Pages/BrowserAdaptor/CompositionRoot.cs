using Dfe.Testing.Pages.BrowserAdaptor.Contracts;
using Dfe.Testing.Pages.BrowserAdaptor.WebDriver;

namespace Dfe.Testing.Pages.BrowserAdaptor;
public static class CompositionRoot
{
    public static IServiceCollection AddWebDriver(this IServiceCollection services)
    {
        services.AddScoped<IBrowserAdaptor, WebDriverBrowserAdaptor>();
        return services;
    }
}
