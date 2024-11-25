namespace Dfe.Testing.Pages.Public.Mapper.Interface;
public interface IComponentMapper<TOut>
    where TOut : IComponent
{
    TOut Map(IDocumentPart input);
}
