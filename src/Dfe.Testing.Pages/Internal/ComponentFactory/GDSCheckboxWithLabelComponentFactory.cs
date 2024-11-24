using Dfe.Testing.Pages.Public.DocumentQueryClient.Pages.Components;

namespace Dfe.Testing.Pages.Internal.ComponentFactory;

internal sealed class GDSCheckboxWithLabelComponentFactory : ComponentFactory<GDSCheckboxWithLabel>
{
    internal static IElementSelector GDSCheckboxItemStyle => new CssSelector(".govuk-checkboxes__item");
    internal static IElementSelector GDSCheckboxInputStyle => new CssSelector(".govuk-checkboxes__input");
    internal static IElementSelector GDSLabelStyle => new CssSelector(".govuk-checkboxes__label");

    public GDSCheckboxWithLabelComponentFactory(IDocumentQueryClientAccessor documentQueryClientAccessor) : base(documentQueryClientAccessor)
    {
    }

    // TODO Checkbox component
    // TODO label component
    private static Func<IDocumentPart, GDSCheckboxWithLabel> MapCheckboxes =>
        (checkboxItemWithLabel) =>
        {
            var checkboxItem = checkboxItemWithLabel.GetChild(GDSCheckboxInputStyle) ?? throw new ArgumentNullException(nameof(GDSCheckboxInputStyle));
            var checkboxLabel = checkboxItemWithLabel.GetChild(GDSLabelStyle) ?? throw new ArgumentNullException(nameof(GDSLabelStyle));

            return new GDSCheckboxWithLabel()
            {
                TagName = "",
                Name = checkboxItem.GetAttribute("name") ?? throw new ArgumentNullException($"no name of {nameof(checkboxItem)}")!,
                Label = checkboxLabel.Text ?? throw new ArgumentNullException($"no label on {nameof(checkboxItem)}"),
                Value = checkboxItem.GetAttribute("value") ?? throw new ArgumentNullException($"no value on {nameof(checkboxItem)}"),
                Checked = checkboxItem.HasAttribute("checked")
            };
        };

    public override List<GDSCheckboxWithLabel> GetMany(QueryRequestArgs? request = null)
    {
        QueryRequestArgs queryRequest = MergeRequest(request, GDSCheckboxItemStyle);

        return DocumentQueryClient.QueryMany(
                args: queryRequest,
                mapper: MapCheckboxes)
            .ToList();
    }

    internal List<GDSCheckboxWithLabel> GetCheckboxesFromPart(IDocumentPart? part)
        => part?
            .GetChildren(GDSCheckboxItemStyle)?
            .Select(MapCheckboxes)
            .ToList() ?? throw new ArgumentNullException(nameof(part));
}
