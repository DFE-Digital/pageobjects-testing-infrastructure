namespace Dfe.Testing.Pages.Internal.Components;
public interface IComponentMapper<Tin, TOut>
    where Tin : IDocumentPart
    where TOut : IComponent
{
    TOut Map(Tin input);
}
