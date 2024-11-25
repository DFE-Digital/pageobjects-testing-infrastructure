using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dfe.Testing.Pages.Internal.Components.AnchorLink;
using Dfe.Testing.Pages.Internal.Components.Button;
using Dfe.Testing.Pages.Internal.Components.Checkbox;
using Dfe.Testing.Pages.Internal.Components.Form;
using Dfe.Testing.Pages.Public.DocumentQueryClient.Pages.Components;

namespace Dfe.Testing.Pages.Internal.Components;
internal static class DependencyInjection
{
    internal static IServiceCollection AddComponents(this IServiceCollection services)
    {
        // anchor link
        services
        .AddTransient<ComponentFactory<AnchorLinkComponent>, AnchorLinkComponentFactory>()
        .AddTransient<IComponentMapper<AnchorLinkComponent>, AnchorLinkMapper>()
        // form
        .AddTransient<ComponentFactory<FormComponent>, FormFactory>()
        .AddTransient<IComponentMapper<FormComponent>, FormMapper>()
        // header
        .AddTransient<ComponentFactory<GDSHeader>, GDSHeaderFactory>()
        // fieldset
        .AddTransient<ComponentFactory<GDSFieldsetComponent>, GDSFieldsetFactory>()
        // checkboxes
        .AddTransient<ComponentFactory<GDSCheckboxWithLabelComponent>, GDSCheckboxWithLabelFactory>()
        .AddTransient<IComponentMapper<GDSCheckboxWithLabelComponent>, GDSCheckboxMapper>()
        // button
        .AddTransient<ComponentFactory<GDSButtonComponent>, GDSButtonFactory>()
        .AddTransient<IComponentMapper<GDSButtonComponent>, GDSButtonMapper>()
        // cookie banner
        .AddTransient<ComponentFactory<GDSCookieBanner>, GDSCookieBannerFactory>();
        return services;
    }
}
