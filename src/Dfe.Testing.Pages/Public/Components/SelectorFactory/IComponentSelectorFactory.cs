namespace Dfe.Testing.Pages.Public.Components.SelectorFactory;
public interface IComponentSelectorFactory
{
    IElementSelector GetSelector<TComponent>() where TComponent : class;
    IElementSelector GetSelector(Type component);
    IElementSelector GetSelector(string pageName);
}
