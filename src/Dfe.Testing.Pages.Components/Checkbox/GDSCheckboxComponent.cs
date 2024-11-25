using Dfe.Testing.Pages.Public.DocumentQueryClient;

namespace Dfe.Testing.Pages.Components.Checkbox;

public sealed record GDSCheckboxComponent : IComponent
{
    public required string TagName { get; init; }
    public required string Name { get; init; }
    public required string Value { get; init; }
    public required string Label { get; init; }
    public bool Checked { get; init; } = false;
}
