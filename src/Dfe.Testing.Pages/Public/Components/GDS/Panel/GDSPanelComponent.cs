using Dfe.Testing.Pages.Public.Components.Text;

namespace Dfe.Testing.Pages.Public.Components.GDS.Panel;
public record GDSPanelComponent
{
    public required TextComponent? Heading { get; init; }
    public required TextComponent? Content { get; init; }
}
