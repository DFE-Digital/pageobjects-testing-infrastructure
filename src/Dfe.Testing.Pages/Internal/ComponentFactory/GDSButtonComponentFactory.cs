using Dfe.Testing.Pages.Public.DocumentQueryClient.Pages.Components;

namespace Dfe.Testing.Pages.Internal.ComponentFactory;

internal sealed class GDSButtonComponentFactory : ComponentFactory<GDSButton>
{
    internal static IElementSelector DefaultButtonStyleSelector => new CssSelector(".govuk-button");
    internal static IElementSelector SecondaryButtonStyle => new CssSelector("govuk-button--secondary");
    internal static IElementSelector WarningButtonStyle => new CssSelector(".govuk-button--warning");
    public GDSButtonComponentFactory(IDocumentQueryClientAccessor documentQueryClientAccessor) : base(documentQueryClientAccessor)
    {
    }

    public override List<GDSButton> GetMany(QueryRequestArgs? request = null)
    {
        QueryRequestArgs queryRequest = MergeRequest(request, DefaultButtonStyleSelector);

        return DocumentQueryClient.QueryMany(
            args: queryRequest,
            mapper: (part) =>
            {
                var classStyles = part.GetAttribute("class") ?? string.Empty;
                return new GDSButton()
                {
                    ButtonType =
                        classStyles.Contains(SecondaryButtonStyle.ToSelector()) ? ButtonStyleType.Secondary :
                        classStyles.Contains(WarningButtonStyle.ToSelector()) ? ButtonStyleType.Warning : ButtonStyleType.Primary,
                    Text = part.Text?.Trim() ?? string.Empty,
                    TagName = part.TagName,
                    Disabled = part.HasAttribute("disabled"),
                    IsSubmit = part.GetAttribute("type") == "submit"
                };
            }).ToList();
    }
}
