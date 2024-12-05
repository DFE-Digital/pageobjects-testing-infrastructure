namespace Dfe.Testing.Pages.Public.Components.Link;
public sealed class AnchorLinkMapper : IComponentMapper<AnchorLinkComponent>
{
    public AnchorLinkComponent Map(IDocumentPart input)
    {
        return new()
        {
            LinkedTo = input.GetAttribute("href")! ?? string.Empty,
            Text = input.Text.Trim() ?? string.Empty,
            OpensInNewTab = input.GetAttribute("target") == "_blank"
        };
    }
}
