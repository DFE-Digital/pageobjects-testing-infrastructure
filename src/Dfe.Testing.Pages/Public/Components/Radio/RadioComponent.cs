namespace Dfe.Testing.Pages.Public.Components.Radio;
public record class RadioComponent
{
    public string Id { get; init; } = string.Empty;
    public required string Name { get; init; }
    public required string Value { get; init; }
    public bool IsRequired { get; init; } = false;
}
