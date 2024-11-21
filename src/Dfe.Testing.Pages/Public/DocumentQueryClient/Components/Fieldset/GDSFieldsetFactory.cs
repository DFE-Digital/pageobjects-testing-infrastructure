namespace Dfe.Testing.Pages.Public.DocumentQueryClient.Components.Fieldset;

public sealed class GDSFieldsetFactory : ComponentFactoryBase<GDSFieldset>
{
    private readonly GDSCheckboxWithLabelFactory _checkboxWithLabelComponent;

    public GDSFieldsetFactory(
        IDocumentQueryClientAccessor documentQueryClientAccessor,
        GDSCheckboxWithLabelFactory checkboxWithLabelComponent) : base(documentQueryClientAccessor)
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
