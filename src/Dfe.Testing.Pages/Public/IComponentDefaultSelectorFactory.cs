namespace Dfe.Testing.Pages.Public;
public interface IComponentDefaultSelectorFactory
{
    IElementSelector GetSelector<TComponent>() where TComponent : IComponent;
    IElementSelector GetSelector(Type component);
    IElementSelector GetSelector(string pageName);
}
