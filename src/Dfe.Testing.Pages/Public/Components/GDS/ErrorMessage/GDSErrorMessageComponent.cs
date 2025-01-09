using Dfe.Testing.Pages.Public.Components.Text;

namespace Dfe.Testing.Pages.Public.Components.GDS.ErrorMessage;
public record GDSErrorMessageComponent
{
    public required TextComponent ErrorMessage { get; init; } = default!;
}
