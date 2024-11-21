namespace Dfe.Testing.Pages.Public.DocumentQueryClient.Components.Buttons;

public sealed class GDSButtonFactory : ComponentFactoryBase<GDSButton>
{
    internal static IElementSelector ButtonStyle => new CssSelector(".govuk-button");
    internal static IElementSelector SecondaryButtonStyle => new CssSelector("govuk-button--secondary");
    internal static IElementSelector WarningButtonStyle => new CssSelector(".govuk-button--warning");
    public GDSButtonFactory(IDocumentQueryClientAccessor documentQueryClientAccessor) : base(documentQueryClientAccessor)
    {
    }

    public override List<GDSButton> GetMany(QueryRequest? request = null)
    {
        QueryRequest queryRequest = new()
        {
            Query = request?.Query ?? ButtonStyle,
            Scope = request?.Scope
        };

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
                    Type = part.GetAttribute("type") ?? string.Empty
                };
            }).ToList();
    }
}
