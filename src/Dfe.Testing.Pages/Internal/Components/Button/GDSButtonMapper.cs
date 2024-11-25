using Dfe.Testing.Pages.Public.DocumentQueryClient.Components;

namespace Dfe.Testing.Pages.Internal.Components.Button;
internal class GDSButtonMapper : IComponentMapper<GDSButtonComponent>
{
    internal static IElementSelector SecondaryButtonStyle => new CssSelector("govuk-button--secondary");
    internal static IElementSelector WarningButtonStyle => new CssSelector(".govuk-button--warning");
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
            TagName = part.TagName,
            Disabled = part.HasAttribute("disabled"),
            IsSubmit = part.GetAttribute("type") == "submit",
            Name = part.GetAttribute("name") ?? string.Empty,
            Value = part.GetAttribute("value") ?? string.Empty
        };
    }
}
