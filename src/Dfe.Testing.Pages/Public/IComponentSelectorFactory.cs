namespace Dfe.Testing.Pages.Public;
public interface IComponentSelectorFactory
{
    IElementSelector GetSelector<TComponent>() where TComponent : IComponent;
    IElementSelector GetSelector(Type component);
    IElementSelector GetSelector(string pageName);
}
