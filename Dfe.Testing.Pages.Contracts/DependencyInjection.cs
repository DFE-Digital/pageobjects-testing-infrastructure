using Dfe.Testing.Pages.Contracts.PageObjectClient.Templates;
using Microsoft.Extensions.DependencyInjection;

namespace Dfe.Testing.Pages.Contracts;
public static class DependencyInjection
{
    public static IServiceCollection AddContracts(this IServiceCollection services)
    {
        services.AddSingleton<IPageObjectTemplateFactory, PageObjectTemplateFactory>();
        return services;
    }
}
