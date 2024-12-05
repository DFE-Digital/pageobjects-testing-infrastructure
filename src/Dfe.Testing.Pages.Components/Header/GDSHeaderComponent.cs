namespace Dfe.Testing.Pages.Components.Header;
public record GDSHeaderComponent : IComponent
{
    public required AnchorLinkComponent GovUKLink { get; init; }
    public IEnumerable<AnchorLinkComponent> NavigationLinks { get; init; } = [];
}
