namespace Dfe.Testing.Pages.Public.Components.HiddenInput;
public record HiddenInputComponent
{
    public required string Name { get; init; }
    public string Value { get; init; } = string.Empty;
}
