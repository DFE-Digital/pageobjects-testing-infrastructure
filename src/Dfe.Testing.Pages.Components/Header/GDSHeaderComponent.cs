using Dfe.Testing.Pages.Components.AnchorLink;
using Dfe.Testing.Pages.Public.DocumentQueryClient;

namespace Dfe.Testing.Pages.Components.Header;
public record GDSHeaderComponent : IComponent
{
    public required AnchorLinkComponent GovUKLink { get; init; }
    public required AnchorLinkComponent ServiceName { get; init; }
    public required string TagName { get; init; }
}
