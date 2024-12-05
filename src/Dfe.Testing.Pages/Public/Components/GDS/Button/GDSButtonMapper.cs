namespace Dfe.Testing.Pages.Public.Components.GDS.Button;
internal class GDSButtonMapper : IComponentMapper<GDSButtonComponent>
{
    internal static IElementSelector SecondaryButtonStyle => new CssElementSelector("govuk-button--secondary");
    internal static IElementSelector WarningButtonStyle => new CssElementSelector(".govuk-button--warning");
    public GDSButtonComponent Map(IDocumentPart part)
    {
        var classStyles = part.GetAttribute("class") ?? string.Empty;
        return new GDSButtonComponent()
        {
            ButtonType =
                classStyles.Contains(SecondaryButtonStyle.ToSelector()) ? ButtonStyleType.Secondary :
                classStyles.Contains(WarningButtonStyle.ToSelector()) ? ButtonStyleType.Warning
                    : ButtonStyleType.Primary,
            Text = part.Text?.Trim() ?? string.Empty,
            Disabled = part.HasAttribute("disabled"),
            IsSubmit = part.GetAttribute("type") == "submit",
            Name = part.GetAttribute("name") ?? string.Empty,
            Value = part.GetAttribute("value") ?? string.Empty
        };
    }
}
