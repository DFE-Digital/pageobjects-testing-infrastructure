namespace Dfe.Testing.Pages.Public.Selector.Factory;
public sealed class ComponentSelectorFactory : IComponentSelectorFactory
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

        return !_mapping.TryGetValue(componentName, out var selector) || selector is null ?
                throw new ArgumentOutOfRangeException(
                    $"Selector for {componentName} is not registered.") : selector();
    }
}
