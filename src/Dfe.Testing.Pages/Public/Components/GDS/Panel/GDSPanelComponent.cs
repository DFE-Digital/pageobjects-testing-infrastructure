using Dfe.Testing.Pages.Public.Components.Core.Text;

namespace Dfe.Testing.Pages.Public.Components.GDS.Panel;
public record GDSPanelComponent
{
    public required TextComponent Heading { get; init; }
    public required TextComponent Content { get; init; }
}
