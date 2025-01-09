namespace Dfe.Testing.Pages.Public.Components.MappingAbstraction.Request;
internal sealed class MapRequestFactory : IMapRequestFactory
{
    public IMapRequest<IDocumentSection> Create(
        IDocumentSection mapFrom,
        IList<IMappingResult> mappings,
        IElementSelector? mappingEntryPoint = null)
    {
        return new DocumentSectionMapRequest()
        {
            From = mapFrom,
            MappingResults = mappings,
            EntryPoint = mappingEntryPoint
        };
    }

    private sealed class DocumentSectionMapRequest : IMapRequest<IDocumentSection>
    {
        public required IDocumentSection From { get; init; }
        public IElementSelector? EntryPoint { get; init; } = null;
        public IList<IMappingResult> MappingResults { get; init; } = [];
    }
}
