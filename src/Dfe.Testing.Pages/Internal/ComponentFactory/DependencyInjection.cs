using Dfe.Testing.Pages.Components.Button;
using Dfe.Testing.Pages.Components.Checkbox;
using Dfe.Testing.Pages.Components.CookieBanner;
using Dfe.Testing.Pages.Components.Fieldset;
using Dfe.Testing.Pages.Components.Form;
using Dfe.Testing.Pages.Components.TextInput;
using Dfe.Testing.Pages.Internal.ComponentFactory.AnchorLink;
using Dfe.Testing.Pages.Internal.ComponentFactory.Button;
using Dfe.Testing.Pages.Internal.ComponentFactory.Checkbox;
using Dfe.Testing.Pages.Internal.ComponentFactory.CookieBanner;
using Dfe.Testing.Pages.Internal.ComponentFactory.Fieldset;
using Dfe.Testing.Pages.Internal.ComponentFactory.Form;
using Dfe.Testing.Pages.Internal.ComponentFactory.Header;
using Dfe.Testing.Pages.Internal.ComponentFactory.TextInput;

namespace Dfe.Testing.Pages.Internal.ComponentFactory;
internal static class DependencyInjection
{
    internal static IServiceCollection AddComponents(this IServiceCollection services)
    {
        // anchor link
        services
            .AddSingleton<IComponentSelectorFactory, ComponentSelectorFactory>((sp) =>
            {
                Dictionary<string, Func<IElementSelector>> componentSelectorMapping = new()
                {
                    { nameof(AnchorLinkComponent), () => new CssSelector("a")},
                    { nameof(GDSHeaderComponent), () => new CssSelector("header.govuk-header")},
                    { nameof(GDSFieldsetComponent), () => new CssSelector("fieldset.govuk-fieldset")},
                    { nameof(GDSCheckboxWithLabelComponent), () => new CssSelector("input.govuk-checkbox")},
                    { nameof(GDSButtonComponent), () => new CssSelector(".govuk-button")},
                    { nameof(GDSTextInputComponent), () => new CssSelector("input.govuk-input")},
                    { nameof(GDSCookieBannerComponent), () => new CssSelector("div.govuk-cookie-banner")},
                    { nameof(FormComponent), () => new CssSelector("form")},
                };

                return new ComponentSelectorFactory(componentSelectorMapping);
            })
        // anchor link
        .AddTransient<ComponentFactory<AnchorLinkComponent>>()
        .AddTransient<IComponentMapper<AnchorLinkComponent>, AnchorLinkMapper>()
        // button
        .AddTransient<ComponentFactory<GDSButtonComponent>>()
        .AddTransient<IComponentMapper<GDSButtonComponent>, GDSButtonMapper>()
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
        // text input
        .AddTransient<ComponentFactory<GDSTextInputComponent>, GDSTextInputFactory>()
        .AddTransient<IComponentMapper<GDSTextInputComponent>, GDSTextInputMapper>()
        // cookie banner
        .AddTransient<ComponentFactory<GDSCookieBannerComponent>, GDSCookieBannerFactory>()
        .AddTransient<IComponentMapper<GDSCookieBannerComponent>, GDSCookieBannerMapper>();
        return services;
    }
}

public interface IComponentSelectorFactory
{
    IElementSelector GetSelector<TComponent>() where TComponent : IComponent;
    IElementSelector GetSelector(Type component);
    IElementSelector GetSelector(string pageName);
}

internal sealed class ComponentSelectorFactory : IComponentSelectorFactory
{
    private readonly IDictionary<string, Func<IElementSelector>> _mapping;

    public ComponentSelectorFactory(IDictionary<string, Func<IElementSelector>> mapping)
    {
        ArgumentNullException.ThrowIfNull(mapping);
        _mapping = mapping;
    }

    public IElementSelector GetSelector<TComponent>() where TComponent : IComponent => GetSelector(typeof(TComponent));

    public IElementSelector GetSelector(Type component) => GetSelector(component.Name);

    public IElementSelector GetSelector(string componentName)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(componentName);

        return (!_mapping.TryGetValue(componentName, out var selector) || selector is null) ?
                throw new ArgumentOutOfRangeException(
                    $"Selector for {componentName} is not registered.") : selector();
    }
}
