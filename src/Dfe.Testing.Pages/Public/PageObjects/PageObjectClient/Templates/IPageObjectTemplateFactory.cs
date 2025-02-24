namespace Dfe.Testing.Pages.Public.PageObjects.PageObjectClient.Templates;
public interface IPageObjectTemplateFactory
{
    IPageObjectTemplate GetTemplateById(string templateId);
    IPageObjectTemplate GetTemplateForType<T>() where T : class;
    IPageObjectTemplate GetTemplateForType(Type componentType);
}

internal sealed class PageObjectTemplateFactory : IPageObjectTemplateFactory
{
    private readonly IEnumerable<IPageObjectTemplate> _pageObjectTemplates;

    public PageObjectTemplateFactory(IEnumerable<IPageObjectTemplate> pageObjectTemplates)
    {
        ArgumentNullException.ThrowIfNull(pageObjectTemplates);
        _pageObjectTemplates = pageObjectTemplates;
    }

    public IPageObjectTemplate GetTemplateById(string templateId)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(templateId);
        var templates = _pageObjectTemplates.Where(t => t.Id == templateId);
        if (!templates.Any())
        {
            throw new ArgumentException($"Unable to find template with id: {templateId}");
        }
        if (templates.Count() > 1)
        {
            throw new ArgumentException($"Found multiple templates with id: {templateId}");
        }
        return templates.Single();
    }

    public IPageObjectTemplate GetTemplateForType<T>() where T : class => GetTemplateForType(typeof(T));

    public IPageObjectTemplate GetTemplateForType(Type componentType) => GetTemplateById(componentType.Name);
}
