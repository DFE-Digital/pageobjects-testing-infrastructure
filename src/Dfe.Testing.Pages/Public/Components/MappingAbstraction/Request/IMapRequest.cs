namespace Dfe.Testing.Pages.Public.Components.MappingAbstraction.Request;
internal interface IMapRequest<Tin>
{
    Tin Document { get; }
    MappingOptions Options { get; }
    IList<IMappingResult> MappedResults { get; }
}

internal sealed class MappingOptions
{
    public required MapKey MapKey { get; init; }
    public IDictionary<string, IElementSelector> OverrideMapperConfiguration { get; init; } = new Dictionary<string, IElementSelector>();
    public IElementSelector? OverrideMapperEntrypoint { get; init; } = null;
}

internal sealed class DocumentSectionMapRequest : IMapRequest<IDocumentSection>
{
    public required IDocumentSection Document { get; init; }
    public required MappingOptions Options { get; init; }
    public required IList<IMappingResult> MappedResults { get; init; } = [];
}
