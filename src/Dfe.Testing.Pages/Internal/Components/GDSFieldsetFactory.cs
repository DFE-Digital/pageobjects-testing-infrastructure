using Dfe.Testing.Pages.Public.DocumentQueryClient.Pages.Components;

namespace Dfe.Testing.Pages.Internal.Components;

internal sealed class GDSFieldsetFactory : ComponentFactory<GDSFieldsetComponent>
{
    private readonly GDSCheckboxWithLabelFactory _checkboxWithLabelComponent;

    public GDSFieldsetFactory(
        IDocumentQueryClientAccessor documentQueryClientAccessor,
        GDSCheckboxWithLabelFactory checkboxWithLabelComponent) : base(documentQueryClientAccessor)
    {
        _checkboxWithLabelComponent = checkboxWithLabelComponent;
    }

    public override List<GDSFieldsetComponent> GetMany(QueryRequestArgs? request = null)
    {
        var queryRequest = MergeRequest(request, new CssSelector("fieldset"));

        return DocumentQueryClient.QueryMany(
            queryRequest,
            mapper: (part) => new GDSFieldsetComponent()
            {
                TagName = part.TagName,
                Legend = part.GetChild(new CssSelector("legend"))?.Text ?? throw new ArgumentNullException("legend on fieldset is null"),
                Checkboxes = _checkboxWithLabelComponent.GetCheckboxesFromPart(part)
            }).ToList();
    }
}
