namespace Dfe.Testing.Pages.Internal.ComponentFactory;
public interface IComponentMapper<TOut>
    where TOut : IComponent
{
    TOut Map(IDocumentPart input);
}
