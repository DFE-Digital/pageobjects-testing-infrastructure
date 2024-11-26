namespace Dfe.Testing.Pages.Public.Mapper.Abstraction;
public interface IComponentMapper<TOut>
    where TOut : IComponent
{
    TOut Map(IDocumentPart input);
}
