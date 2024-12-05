namespace Dfe.Testing.Pages.Public.Components.Inputs;
public record class RadioComponent : IComponent
{
    public string Id { get; init; } = string.Empty;
    public required string Name { get; init; }
    public required string Value { get; init; }
    public bool IsRequired { get; init; } = false;
}
