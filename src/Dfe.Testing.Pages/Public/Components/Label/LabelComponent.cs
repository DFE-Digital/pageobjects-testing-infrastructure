using Dfe.Testing.Pages.Public.Components.Text;

namespace Dfe.Testing.Pages.Public.Components.Label;
public record LabelComponent
{
    public required string For { get; init; }
    public required TextComponent? Text { get; init; }
}
