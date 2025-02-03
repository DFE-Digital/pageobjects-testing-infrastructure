namespace Dfe.Testing.Pages.Public.Components.EntrypointSelectorFactory;
public sealed class EntrypointSelectorFactory : IEntrypointSelectorFactory
{
    private readonly IDictionary<string, Func<IElementSelector?>> _selectorMap;

    public EntrypointSelectorFactory(IDictionary<string, Func<IElementSelector?>> mapping)
    {
        ArgumentNullException.ThrowIfNull(mapping);
        _selectorMap = mapping;
    }

    public IElementSelector? GetSelector<TComponent>() where TComponent : class => GetSelector(typeof(TComponent));

    public IElementSelector? GetSelector(Type component) => GetSelector(component.Name);

    public IElementSelector? GetSelector(string key)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(key);
        return !_selectorMap.TryGetValue(key, out var getSelectorFunc) ||
            getSelectorFunc is null ? null : getSelectorFunc();
    }
}
