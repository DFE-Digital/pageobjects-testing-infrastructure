namespace Dfe.Testing.Pages.Public.Components.MappingAbstraction;

internal interface IComponentMapper<TComponent> : IMapper<IMapRequest<IDocumentSection>, MappedResponse<TComponent>> where TComponent : class
{
}
