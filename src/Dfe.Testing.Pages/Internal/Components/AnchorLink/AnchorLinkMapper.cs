using Dfe.Testing.Pages.Public.DocumentQueryClient.Pages.Components;

namespace Dfe.Testing.Pages.Internal.Components.AnchorLink;
public sealed class AnchorLinkMapper : IComponentMapper<IDocumentPart, AnchorLinkComponent>
{
    public AnchorLinkComponent Map(IDocumentPart input)
    {
        return new()
        {
            LinkValue = input.GetAttribute("href")!,
            Text = input.Text.Trim(),
            OpensInNewTab = input.GetAttribute("target") == "_blank"
        };
    }
}
