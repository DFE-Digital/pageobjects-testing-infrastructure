using Dfe.Testing.Pages.Public.Components.Core.Text;

namespace Dfe.Testing.Pages.Public.Components.Core.Label;
public record LabelComponent
{
    public required string For { get; init; }
    public required TextComponent Text { get; init; }
}
