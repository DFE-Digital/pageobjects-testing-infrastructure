using Dfe.Testing.Pages.Internal.DocumentClient;
using Dfe.Testing.Pages.Public.Components.EntrypointSelectorFactory;

namespace Dfe.Testing.Pages.Public.Components;

public interface IComponentFactory<T> where T : class
{
    CreatedComponentResponse<T> Create(CreateComponentRequest? request = null);
    IEnumerable<CreatedComponentResponse<T>> CreateMany(CreateComponentRequest? request = null);
}

internal sealed class ComponentFactory<T> : IComponentFactory<T> where T : class
{
    private readonly IDocumentService _documentClient;
    private readonly IComponentMapper<T> _mapper;
    private readonly IEntrypointSelectorFactory _componentSelectorFactory;

    public ComponentFactory(
        IDocumentService documentClient,
        IComponentMapper<T> mapper,
        IEntrypointSelectorFactory componentSelectorFactory)
    {
        _documentClient = documentClient;
        _mapper = mapper;
        _componentSelectorFactory = componentSelectorFactory;
    }

    public CreatedComponentResponse<T> Create(CreateComponentRequest? request = null) => CreateMany(request).Single();

    public IEnumerable<CreatedComponentResponse<T>> CreateMany(CreateComponentRequest? request = null)
    {
        FindOptions mergedFindOptions = new()
        {
            FindInScope = request?.FindInScope ?? null,
            Selector = request?.Selector ?? _componentSelectorFactory.GetSelector<T>()
        };

        // Query
        IEnumerable<IDocumentSection> documentSections = _documentClient.ExecuteQuery(mergedFindOptions) ?? [];


        // Map
        IEnumerable<MappedResponse<T>> mappedComponentResponsesFromDocumentSections =
            documentSections.Select((section) =>
            {
                ArgumentNullException.ThrowIfNull(section, $"section returned as null in DocumentClient query");
                string componentRequestedType = typeof(T).Name;
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
                MappedResponse<T> response = _mapper.Map(mapRequest);
                return response;
            });

        // Generate CreatedComponentResponse
        return mappedComponentResponsesFromDocumentSections.Select(
            (mappedResponse) =>
            {
                List<IMappingResult> outputResults = [];
                outputResults.Add(mappedResponse.MappingResult);

                return new CreatedComponentResponse<T>()
                {
                    Created = mappedResponse.Mapped!,
                    CreatingComponentResults = outputResults
                };
            });
    }
}
