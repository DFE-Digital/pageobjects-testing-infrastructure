using Dfe.Testing.Pages.Components.AnchorLink;
using Dfe.Testing.Pages.Public.DocumentQueryClient;

namespace Dfe.Testing.Pages.Components.ErrorSummary;
public record GDSErrorSummaryComponent : IComponent
{
    public required string Heading { get; init; }
    public required IEnumerable<AnchorLinkComponent> Errors { get; init; }
}
