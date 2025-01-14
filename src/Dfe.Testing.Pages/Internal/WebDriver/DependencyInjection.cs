using Dfe.Testing.Pages.Internal.WebDriver.Provider.Factory;

namespace Dfe.Testing.Pages.Internal.WebDriver;

internal static class DependencyInjection
{
    internal static IServiceCollection AddWebDriverServices(this IServiceCollection services)
        => services
            .AddPublicAPI();

    private static IServiceCollection AddPublicAPI(this IServiceCollection services)
        =>
        services
            .AddScoped<IWebDriverAdaptor, CachedWebDriverAdaptor>()
            .AddSingleton<IBrowserFactory, ChromeDriverFactory>();
    //.AddScoped<IWebDriverAdaptorProvider, CachedWebDriverAdaptorProvider>();
}
