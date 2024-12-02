namespace Dfe.Testing.Pages.Components.Inputs.TextInput;
public record HiddenInputComponent : IComponent
{
    public required string Name { get; init; }
    public string Value { get; init; } = string.Empty;
}
