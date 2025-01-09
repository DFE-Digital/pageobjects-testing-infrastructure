namespace Dfe.Testing.Pages.Public.Components.MappingAbstraction.Request;
public interface IMapRequest<Tin>
{
    Tin From { get; }
    IElementSelector? EntryPoint { get; }
    IList<IMappingResult> MappingResults { get; }
    // TODO shallow clone with overrideable ComponentEntryPoint?
}

// TODO generic so that any TIn can be passed.
public interface IMapRequestFactory
{
    IMapRequest<IDocumentSection> Create(
        IDocumentSection mapFrom,
        IList<IMappingResult> mappings,
        IElementSelector? mappingEntryPoint = null);
}
