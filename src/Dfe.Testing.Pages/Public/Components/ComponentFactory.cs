using Dfe.Testing.Pages.Internal.DocumentClient;
using Dfe.Testing.Pages.Public.Components.MappingAbstraction.Request;
using Dfe.Testing.Pages.Public.Components.SelectorFactory;

namespace Dfe.Testing.Pages.Public.Components;
public class ComponentFactory<T> where T : class
{
    private readonly IComponentSelectorFactory _componentSelectorFactory;
    private readonly IMapRequestFactory _mapRequestFactory;
    private readonly IDocumentService _documentClient;
    private readonly IMapper<IMapRequest<IDocumentSection>, MappedResponse<T>> _mapper;

    public ComponentFactory(
        IMapRequestFactory mapRequestFactory,
        IDocumentService documentClient,
        IMapper<IMapRequest<IDocumentSection>, MappedResponse<T>> mapper,
        IComponentSelectorFactory componentSelectorFactory)
    {
        _mapRequestFactory = mapRequestFactory;
        _documentClient = documentClient;
        _mapper = mapper;
        _componentSelectorFactory = componentSelectorFactory;
    }

    public virtual CreatedComponentResponse<T> Create(CreateComponentRequest? request = null) => CreateMany(request).Single();

    public virtual IList<CreatedComponentResponse<T>> CreateMany(CreateComponentRequest? request = null)
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
                MappedResponse<T> response = _mapper.Map(
                    _mapRequestFactory.Create(
                        mapFrom: section,
                        mappings: []));
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
            }).ToList();
    }
}
