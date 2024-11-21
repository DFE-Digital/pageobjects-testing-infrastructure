namespace Dfe.Testing.Pages.Public.DocumentQueryClient.Components.CheckboxWithLabel;

public sealed class GDSCheckboxWithLabelFactory : ComponentFactoryBase<GDSCheckboxWithLabel>
{
    internal static IElementSelector Checkbox => new CssSelector(".govuk-checkboxes__item");
    internal static IElementSelector Input => new CssSelector(".govuk-checkboxes__input");
    internal static IElementSelector Label => new CssSelector(".govuk-checkboxes__label");

    public GDSCheckboxWithLabelFactory(IDocumentQueryClientAccessor documentQueryClientAccessor) : base(documentQueryClientAccessor)
    {
    }

    // TODO Checkbox component
    // TODO label component
    private static Func<IDocumentPart, GDSCheckboxWithLabel> MapCheckboxes =>
        (checkboxItemWithLabel) =>
        {
            var checkboxItem = checkboxItemWithLabel.GetChild(Input) ?? throw new ArgumentNullException(nameof(Input));
            var checkboxLabel = checkboxItemWithLabel.GetChild(Label) ?? throw new ArgumentNullException(nameof(Label));

            return new GDSCheckboxWithLabel()
            {
                TagName = "",
                Name = checkboxItem.GetAttribute("name") ?? throw new ArgumentNullException($"no name of {nameof(checkboxItem)}")!,
                Label = checkboxLabel.Text ?? throw new ArgumentNullException($"no label on {nameof(checkboxItem)}"),
                Value = checkboxItem.GetAttribute("value") ?? throw new ArgumentNullException($"no value on {nameof(checkboxItem)}"),
                Checked = checkboxItem.HasAttribute("checked")
            };
        };

    public override List<GDSCheckboxWithLabel> GetMany(QueryRequest? request = null)
    {
        QueryRequest queryRequest = new()
        {
            Query = request?.Query ?? Checkbox,
            Scope = request?.Scope
        };

        return DocumentQueryClient.QueryMany(
                args: queryRequest,
                mapper: MapCheckboxes)
            .ToList();
    }

    internal List<GDSCheckboxWithLabel> GetCheckboxesFromPart(IDocumentPart? part)
        => part?
            .GetChildren(Checkbox)?
            .Select(MapCheckboxes)
            .ToList() ?? throw new ArgumentNullException(nameof(part));
}
