namespace Dfe.Testing.Pages.Internal.Components;
public interface IComponentMapper<TOut>
    where TOut : IComponent
{
    TOut Map(IDocumentPart input);
}
