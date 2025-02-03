namespace Dfe.Testing.Pages.Public.Components.EntrypointSelectorFactory;
public interface IEntrypointSelectorFactory
{
    IElementSelector? GetSelector<TComponent>() where TComponent : class;
    IElementSelector? GetSelector(Type component);
    IElementSelector? GetSelector(string componentKey);
}
