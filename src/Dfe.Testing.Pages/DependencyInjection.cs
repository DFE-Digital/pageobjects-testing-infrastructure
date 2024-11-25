using System.Linq;
using Dfe.Testing.Pages.Internal;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Dfe.Testing.Pages;

public static class DependencyInjection
{
    public static IServiceCollection AddAngleSharp<TApplicationProgram>(this IServiceCollection services) where TApplicationProgram : class
        => services
            .AddDocumentQueryClient<AngleSharpDocumentQueryClientProvider>()
            .AddWebApplicationFactory<TApplicationProgram>();

    public static IServiceCollection AddWebDriver(this IServiceCollection services)
        => services
            .AddDocumentQueryClient<WebDriverDocumentQueryClientProvider>()
            .AddWebDriverServices();
}
