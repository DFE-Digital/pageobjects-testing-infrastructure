namespace Dfe.Testing.Pages.Public.Components.MappingAbstraction;
internal sealed class SearchDocumentForMappingEntrypointMappingDecorator<T> : IComponentMapper<T> where T : class
{
    private readonly IComponentMapper<T> _decoratoratedComponentMapper;
    private readonly IMapRequestFactory _mapRequestFactory;

    public SearchDocumentForMappingEntrypointMappingDecorator(
        IComponentMapper<T> decoratoedComponentMapper,
        IMapRequestFactory mapRequestFactory)
    {
        _decoratoratedComponentMapper = decoratoedComponentMapper;
        _mapRequestFactory = mapRequestFactory;
    }

    public MappedResponse<T> Map(IMapRequest<IDocumentSection> request)
    {
        ArgumentNullException.ThrowIfNull(request);
        ArgumentNullException.ThrowIfNull(request.Document);

        var mapFrom = (request.Options.OverrideMapperEntrypoint == null ?
                request.Document :
                request.Document.FindDescendant(request.Options.OverrideMapperEntrypoint))
                ?? throw new ArgumentException($"Could not find entrypoint descendant using entrypoint selector {request.Options.OverrideMapperEntrypoint!.ToSelector()} from Document: {Environment.NewLine}{request.Document}.");

        return _decoratoratedComponentMapper.Map(
            _mapRequestFactory.CreateRequestWithDocumentFrom(request, mapFrom));
    }
}
