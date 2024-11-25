using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dfe.Testing.Pages.Internal.Components.AnchorLink;
using Dfe.Testing.Pages.Internal.Components.Button;
using Dfe.Testing.Pages.Internal.Components.Checkbox;
using Dfe.Testing.Pages.Internal.Components.CookieBanner;
using Dfe.Testing.Pages.Internal.Components.Fieldset;
using Dfe.Testing.Pages.Internal.Components.Form;
using Dfe.Testing.Pages.Internal.Components.Header;
using Dfe.Testing.Pages.Public.DocumentQueryClient.Pages.Components;

namespace Dfe.Testing.Pages.Internal.Components;
internal static class DependencyInjection
{
    internal static IServiceCollection AddGDSComponents(this IServiceCollection services)
    {
        // anchor link
        services
        .AddTransient<ComponentFactory<AnchorLinkComponent>, AnchorLinkComponentFactory>()
        .AddTransient<IComponentMapper<AnchorLinkComponent>, AnchorLinkMapper>()
        // form
        .AddTransient<ComponentFactory<FormComponent>, FormFactory>()
        .AddTransient<IComponentMapper<FormComponent>, FormMapper>()
        // header
        .AddTransient<ComponentFactory<GDSHeaderComponent>, GDSHeaderFactory>()
        .AddTransient<IComponentMapper<GDSHeaderComponent>, GDSHeaderMapper>()
        // fieldset
        .AddTransient<ComponentFactory<GDSFieldsetComponent>, GDSFieldsetFactory>()
        .AddTransient<IComponentMapper<GDSFieldsetComponent>, GDSFieldsetMapper>()
        // checkboxes
        .AddTransient<ComponentFactory<GDSCheckboxWithLabelComponent>, GDSCheckboxWithLabelFactory>()
        .AddTransient<IComponentMapper<GDSCheckboxWithLabelComponent>, GDSCheckboxMapper>()
        // button
        .AddTransient<ComponentFactory<GDSButtonComponent>, GDSButtonFactory>()
        .AddTransient<IComponentMapper<GDSButtonComponent>, GDSButtonMapper>()
        // cookie banner
        .AddTransient<ComponentFactory<GDSCookieBannerComponent>, GDSCookieBannerFactory>()
        .AddTransient<IComponentMapper<GDSCookieBannerComponent>, GDSCookieBannerMapper>();
        return services;
    }
}
