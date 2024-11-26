using Dfe.Testing.Pages.Public.Mapper.Abstraction;

namespace Dfe.Testing.Pages.Public.Mapper;
public sealed class AnchorLinkMapper : IComponentMapper<AnchorLinkComponent>
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
