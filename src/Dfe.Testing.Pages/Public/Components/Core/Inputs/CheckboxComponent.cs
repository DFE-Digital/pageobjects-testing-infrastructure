namespace Dfe.Testing.Pages.Public.Components.Core.Inputs;
public record CheckboxComponent
{
    public required string Name { get; init; }
    public required string Value { get; init; }
    public string Id { get; init; } = string.Empty;
    public bool IsChecked { get; init; } = false;
    public bool IsRequired { get; init; } = false;
}
