namespace Dfe.Testing.Pages.Public.Components.GDS.Details;
public record GDSDetailsComponent : IComponent
{
    public required string Summary { get; init; }
    public required string Content { get; init; }
}
