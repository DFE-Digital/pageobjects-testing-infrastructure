using Dfe.Testing.Pages.Public.DocumentQueryClient.Pages.Components;
using HttpMethod = System.Net.Http.HttpMethod;

namespace Dfe.Testing.Pages.Internal.ComponentFactory;

internal sealed class FormComponentFactory : ComponentFactory<Form>
{
    // may not be appropriate if there are multiple forms on the page
    internal static IElementSelector DefaultFormQuery = new CssSelector("form");
    private readonly ComponentFactory<GDSFieldset> _fieldSetFactory;
    private readonly ComponentFactory<GDSButton> _buttonFactory;

    public FormComponentFactory(
        IDocumentQueryClientAccessor documentQueryClientAccessor,
        GDSFieldsetComponentFactory fieldSetComponent,
        GDSButtonComponentFactory buttonFactory) : base(documentQueryClientAccessor)
    {
        ArgumentNullException.ThrowIfNull(fieldSetComponent);
        ArgumentNullException.ThrowIfNull(buttonFactory);
        _fieldSetFactory = fieldSetComponent;
        _buttonFactory = buttonFactory;
    }

    public override List<Form> GetMany(QueryRequestArgs? request = null)
    {
        QueryRequestArgs queryRequest = MergeRequest(request, DefaultFormQuery);

        return DocumentQueryClient.QueryMany(
            queryRequest,
            mapper: (part) => new Form()
            {
                TagName = part.TagName,
                Method = HttpMethod.Parse(part.GetAttribute("method") ?? throw new ArgumentNullException(nameof(Form.Method), "method on form is null")),
                FieldSets = _fieldSetFactory.GetMany(),
                Buttons = _buttonFactory.GetMany(),
                Action = part.GetAttribute("action") ?? throw new ArgumentNullException(nameof(Form.Action), "action on form is null"),
                IsFormValidatedWithHTML = part.HasAttribute("novalidate")
            })
            .ToList();
    }
}
