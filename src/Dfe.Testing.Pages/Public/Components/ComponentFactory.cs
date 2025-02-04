using Dfe.Testing.Pages.Internal.DocumentClient;
using Dfe.Testing.Pages.Public.Components.EntrypointSelectorFactory;

namespace Dfe.Testing.Pages.Public.Components;

public interface IComponentFactory<T> where T : class
{
    CreatedComponentResponse<T> Create(CreateComponentRequest? request = null);
    IEnumerable<CreatedComponentResponse<T>> CreateMany(CreateComponentRequest? request = null);
}

internal sealed class ComponentFactory<TComponent> : IComponentFactory<TComponent> where TComponent : class
{
    private readonly IDocumentService _documentClient;
    private readonly IComponentMapper<TComponent> _mapper;
    private readonly IEntrypointSelectorFactory _componentSelectorFactory;

    public ComponentFactory(
        IDocumentService documentClient,
        IComponentMapper<TComponent> mapper,
        IEntrypointSelectorFactory componentSelectorFactory)
    {
        _documentClient = documentClient;
        _mapper = mapper;
        _componentSelectorFactory = componentSelectorFactory;
    }

    public CreatedComponentResponse<TComponent> Create(CreateComponentRequest? request = null) => CreateMany(request).Single();

    public IEnumerable<CreatedComponentResponse<TComponent>> CreateMany(CreateComponentRequest? request = null)
    {
        FindOptions mergedFindOptions = new()
        {
            FindInScope = request?.FindInScope ?? null,
            Selector = request?.Selector ?? _componentSelectorFactory.GetSelector<TComponent>()
        };

        // Query
        IEnumerable<IDocumentSection> documentSections = _documentClient.ExecuteQuery(mergedFindOptions) ?? [];


        // Map
        IEnumerable<MappedResponse<TComponent>> mappedComponentResponsesFromDocumentSections =
            documentSections.Select((section) =>
            {
                ArgumentNullException.ThrowIfNull(section, $"section returned as null in DocumentClient query");
                string componentRequestedType = typeof(TComponent).Name;
                DocumentSectionMapRequest mapRequest = new()
                {
                    Document = section,
                    MappedResults = [],
                    Options = new()
                    {
                        // adds requested component as type to fulfil MapConfiguration override
                        // e.g {TopLevelComponent e.g FormComponent}{Separator}{Attribute}{Separator}{Attribute} is structure of key looked up in mappers
                        // e.g FormComponent.ViewCookiesLink.Text - TextMapper will use current ChainedLookupKey using the attribute it's currently mapping. This could be a top level map / a nested map.
                        MapConfigurationKey = new MapKey([componentRequestedType]),
                        OverrideMapperConfiguration = request?.Mapping ?? [],
                        // top level entrypoint not changed as Query finds section
                        OverrideMapperEntrypoint = null,
                    }
                };
                MappedResponse<TComponent> response = _mapper.Map(mapRequest);
                return response;
            });

        // Generate CreatedComponentResponse
        return mappedComponentResponsesFromDocumentSections.Select(
            (mappedResponse) =>
            {
                List<IMappingResult> outputResults = [];
                outputResults.Add(mappedResponse.MappingResult);

                return new CreatedComponentResponse<TComponent>()
                {
                    Created = mappedResponse.Mapped!,
                    CreatingComponentResults = outputResults
                };
            });
    }
}
