using Dfe.Testing.Pages.Components.Button;
using Dfe.Testing.Pages.Components.Checkbox;
using Dfe.Testing.Pages.Components.CookieBanner;
using Dfe.Testing.Pages.Components.Fieldset;
using Dfe.Testing.Pages.Components.Form;
using Dfe.Testing.Pages.Internal.ComponentFactory.AnchorLink;
using Dfe.Testing.Pages.Internal.ComponentFactory.Button;
using Dfe.Testing.Pages.Internal.ComponentFactory.Checkbox;
using Dfe.Testing.Pages.Internal.ComponentFactory.CookieBanner;
using Dfe.Testing.Pages.Internal.ComponentFactory.Fieldset;
using Dfe.Testing.Pages.Internal.ComponentFactory.Form;
using Dfe.Testing.Pages.Internal.ComponentFactory.Header;

namespace Dfe.Testing.Pages.Internal.ComponentFactory;
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
