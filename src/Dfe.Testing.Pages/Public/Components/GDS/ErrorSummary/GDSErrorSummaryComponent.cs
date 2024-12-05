using Dfe.Testing.Pages.Public.Components.Link;

namespace Dfe.Testing.Pages.Public.Components.GDS.ErrorSummary;
public record GDSErrorSummaryComponent : IComponent
{
    public required string Heading { get; init; }
    public required IEnumerable<AnchorLinkComponent> Errors { get; init; }
}
