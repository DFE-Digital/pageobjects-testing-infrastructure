using Dfe.Testing.Pages.Internal.ComponentFactory;

namespace Dfe.Testing.Pages.Public.DocumentQueryClient.Components;

internal sealed class GDSFieldsetComponentFactory : ComponentFactory<GDSFieldset>
{
    private readonly GDSCheckboxWithLabelComponentFactory _checkboxWithLabelComponent;

    public GDSFieldsetComponentFactory(
        IDocumentQueryClientAccessor documentQueryClientAccessor,
        GDSCheckboxWithLabelComponentFactory checkboxWithLabelComponent) : base(documentQueryClientAccessor)
    {
        _checkboxWithLabelComponent = checkboxWithLabelComponent;
    }

    public override List<GDSFieldset> GetMany(QueryRequest? request = null)
    {
        QueryRequest queryRequest =
            new()
            {
                Query = request?.Query ?? new CssSelector("fieldset"),
                Scope = request?.Scope
            };

        return DocumentQueryClient.QueryMany(
            queryRequest,
            mapper: (part) => new GDSFieldset()
            {
                TagName = part.TagName,
                Legend = part.GetChild(new CssSelector("legend"))?.Text ?? throw new ArgumentNullException("legend on fieldset is null"),
                Checkboxes = _checkboxWithLabelComponent.GetCheckboxesFromPart(part)
            }).ToList();
    }
}
