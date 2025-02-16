using Dfe.Testing.Pages.Public.Components.Link;
using Dfe.Testing.Pages.Public.Components.Text;

namespace Dfe.Testing.Pages.Public.Components.GDS.ErrorSummary;
public record GDSErrorSummaryComponent
{
    public required TextComponent? Heading { get; init; }
    public required IEnumerable<AnchorLinkComponentOld?> Errors { get; init; }
}
