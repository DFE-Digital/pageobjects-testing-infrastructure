using Dfe.Testing.Pages.Public.Components.Core.Text;

namespace Dfe.Testing.Pages.Public.Components.Core.Link;
public record AnchorLinkComponent
{
    public required string LinkedTo { get; init; }
    public required TextComponent Text { get; init; }
    public bool OpensInNewTab { get; init; } = false;
}
