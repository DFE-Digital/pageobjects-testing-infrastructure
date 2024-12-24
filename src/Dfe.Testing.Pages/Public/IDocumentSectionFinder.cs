namespace Dfe.Testing.Pages.Public;
public interface IDocumentSectionFinder
{
    public IDocumentSection Find<TComponent>(IDocumentSection section) where TComponent : class;
    public IDocumentSection Find(IDocumentSection section, IElementSelector selector);
    public IEnumerable<IDocumentSection> FindMany<TComponent>(IDocumentSection section) where TComponent : class;
    public IEnumerable<IDocumentSection> FindMany(IDocumentSection section, IElementSelector selector);
}
