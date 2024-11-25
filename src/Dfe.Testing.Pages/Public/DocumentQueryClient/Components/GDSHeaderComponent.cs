namespace Dfe.Testing.Pages.Public.DocumentQueryClient.Components;
public record GDSHeaderComponent : IComponent
{
    public required AnchorLinkComponent GovUKLink { get; init; }
    public required AnchorLinkComponent ServiceName { get; init; }
    public required string TagName { get; init; }
}
