using Dfe.Testing.Pages.Public.Components.Core.Link;

namespace Dfe.Testing.Pages.Public.Components.GDS.ErrorSummary;
public record GDSErrorSummaryComponent
{
    public required string Heading { get; init; }
    public required IEnumerable<AnchorLinkComponent> Errors { get; init; }
}
