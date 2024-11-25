namespace Dfe.Testing.Pages.Public.DocumentQueryClient.Pages.Components;

public record GDSFieldsetComponent : IComponent
{
    public required string TagName { get; init; }
    public required string Legend { get; init; }
    public required IEnumerable<GDSCheckboxWithLabel> Checkboxes { get; init; }
}
