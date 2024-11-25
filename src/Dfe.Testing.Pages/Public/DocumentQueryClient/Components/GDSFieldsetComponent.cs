namespace Dfe.Testing.Pages.Public.DocumentQueryClient.Components;

public record GDSFieldsetComponent : IComponent
{
    public required string TagName { get; init; }
    public required string Legend { get; init; }
    public required IEnumerable<GDSCheckboxWithLabelComponent> Checkboxes { get; init; }
}
