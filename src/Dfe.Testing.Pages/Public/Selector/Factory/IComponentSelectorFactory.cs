namespace Dfe.Testing.Pages.Public.Selector.Factory;
public interface IComponentSelectorFactory
{
    IElementSelector GetSelector<TComponent>() where TComponent : IComponent;
    IElementSelector GetSelector(Type component);
    IElementSelector GetSelector(string pageName);
}
