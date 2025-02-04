using Dfe.Testing.Pages.Public.Components.EntrypointSelectorFactory;

namespace Dfe.Testing.Pages.Public.Components.MappingAbstraction.Request;

internal interface IMapRequestFactory
{
    IMapRequest<IDocumentSection> CreateRequestFrom(IMapRequest<IDocumentSection> request, string attributeBeingMapped);
    IMapRequest<IDocumentSection> CreateRequestWithDocumentFrom(IMapRequest<IDocumentSection> request, IDocumentSection mapFromDocument);
}

internal sealed class MapRequestFactory : IMapRequestFactory
{
    private readonly IEntrypointSelectorFactory _entrypointSelectorFactory;

    public MapRequestFactory(IEntrypointSelectorFactory entrypointSelectorFactory)
    {
        _entrypointSelectorFactory = entrypointSelectorFactory;
    }

    public IMapRequest<IDocumentSection> CreateRequestFrom(
        IMapRequest<IDocumentSection> request,
        string attributeBeingMapped)
    {
        // Don't modify existing request as it effects subsequent mappers using request with configKey

        MapKey newMapConfigurationKey = request.Options.MapKey.Append(attributeBeingMapped);
        string configLookupKey = newMapConfigurationKey.ToString();
        request.Options.OverrideMapperConfiguration.TryGetValue(configLookupKey, out IElementSelector? mappingConfiguration);
        IElementSelector? overrideEntrypoint = mappingConfiguration ?? _entrypointSelectorFactory.GetSelector(configLookupKey);

        return new DocumentSectionMapRequest()
        {
            Document = request.Document,
            MappedResults = request.MappedResults,
            Options = new()
            {
                MapKey = newMapConfigurationKey,
                OverrideMapperEntrypoint = overrideEntrypoint,
                OverrideMapperConfiguration = request.Options.OverrideMapperConfiguration
            }
        };
    }

    public IMapRequest<IDocumentSection> CreateRequestWithDocumentFrom(
        IMapRequest<IDocumentSection> request,
        IDocumentSection mapFromDocument)
    {
        return new DocumentSectionMapRequest()
        {
            Document = mapFromDocument,
            MappedResults = request.MappedResults,
            Options = new()
            {
                // Pass everything except OverrideEntrypoint as we are now mapping from a different document point
                MapKey = request.Options.MapKey,
                OverrideMapperConfiguration = request.Options.OverrideMapperConfiguration,
                OverrideMapperEntrypoint = null
            }
        };
    }
}
