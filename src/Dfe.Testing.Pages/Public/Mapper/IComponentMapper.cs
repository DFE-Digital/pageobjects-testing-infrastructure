namespace Dfe.Testing.Pages.Public;
public interface IComponentMapper<TOut>
    where TOut : IComponent
{
    TOut Map(IDocumentPart input);
}
