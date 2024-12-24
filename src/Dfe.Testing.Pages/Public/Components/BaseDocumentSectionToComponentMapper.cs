namespace Dfe.Testing.Pages.Public.Components;
public abstract class BaseDocumentSectionToComponentMapper<TComponentOut> : IMapper<IDocumentSection, TComponentOut>
    where TComponentOut : class
{
    protected readonly IDocumentSectionFinder _documentSectionFinder;

    protected BaseDocumentSectionToComponentMapper(IDocumentSectionFinder documentSectionFinder)
    {
        _documentSectionFinder = documentSectionFinder;
    }

    public abstract TComponentOut Map(IDocumentSection section);
    protected abstract bool IsMappableFrom(IDocumentSection section);
    protected virtual IDocumentSection FindMappableSection<TComponent>(IDocumentSection section) where TComponent : class
    {
        return IsMappableFrom(section) ? section : _documentSectionFinder.Find<TComponent>(section);
    }
}
