namespace Dfe.Testing.Pages.Public.Components.Inputs;
public record HiddenInputComponent : IComponent
{
    public required string Name { get; init; }
    public string Value { get; init; } = string.Empty;
}
