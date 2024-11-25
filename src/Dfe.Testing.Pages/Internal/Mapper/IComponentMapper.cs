namespace Dfe.Testing.Pages.Internal.Mapper;
public interface IComponentMapper<TOut>
    where TOut : IComponent
{
    TOut Map(IDocumentPart input);
}
