namespace Dfe.Testing.Pages.Public.Components.MappingAbstraction.Request;
internal interface IMapRequest<Tin>
{
    Tin Document { get; }
    MappingOptions Options { get; }
    List<IMappingResult> MappedResults { get; }
}

public sealed class MappingOptions
{
    public required MapKey MapConfigurationKey { get; init; }
    public IDictionary<string, IElementSelector> OverrideMapperConfiguration { get; init; } = new Dictionary<string, IElementSelector>();
    public IElementSelector? OverrideMapperEntrypoint { get; init; } = null;
}

internal sealed class DocumentSectionMapRequest : IMapRequest<IDocumentSection>
{
    public required IDocumentSection Document { get; init; }
    public required MappingOptions Options { get; init; }
    public List<IMappingResult> MappedResults { get; init; } = [];
}
