namespace Dfe.Testing.Pages.Public.Components.Core.Inputs;
public record HiddenInputComponent
{
    public required string Name { get; init; }
    public string Value { get; init; } = string.Empty;
}
