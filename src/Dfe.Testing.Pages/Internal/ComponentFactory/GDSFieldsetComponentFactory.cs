using Dfe.Testing.Pages.Public.DocumentQueryClient.Pages.Components;

namespace Dfe.Testing.Pages.Internal.ComponentFactory;

internal sealed class GDSFieldsetComponentFactory : ComponentFactory<GDSFieldset>
{
    private readonly GDSCheckboxWithLabelComponentFactory _checkboxWithLabelComponent;

    public GDSFieldsetComponentFactory(
        IDocumentQueryClientAccessor documentQueryClientAccessor,
        GDSCheckboxWithLabelComponentFactory checkboxWithLabelComponent) : base(documentQueryClientAccessor)
    {
        _checkboxWithLabelComponent = checkboxWithLabelComponent;
    }

    public override List<GDSFieldset> GetMany(QueryRequestArgs? request = null)
    {
        QueryRequestArgs queryRequest = MergeRequest(request, new CssSelector("fieldset"));

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
