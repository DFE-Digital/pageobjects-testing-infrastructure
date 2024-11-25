namespace Dfe.Testing.Pages.Public.DocumentQueryClient.Components;

public sealed record GDSCheckboxWithLabelComponent : IComponent
{
    public required string TagName { get; init; }
    public required string Name { get; init; }
    public required string Value { get; init; }
    public required string Label { get; init; }
    public bool Checked { get; init; } = false;
}
