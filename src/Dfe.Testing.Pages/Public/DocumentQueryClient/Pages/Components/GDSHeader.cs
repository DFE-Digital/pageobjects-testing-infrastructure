namespace Dfe.Testing.Pages.Public.DocumentQueryClient.Pages.Components;
public record GDSHeader : IComponent
{
    public required AnchorLink GovUKLink { get; init; }
    public required AnchorLink ServiceName { get; init; }
    public required string TagName { get; init; }
}
