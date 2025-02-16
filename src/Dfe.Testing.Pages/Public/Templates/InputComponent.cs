namespace Dfe.Testing.Pages.Public.Templates;
public record InputComponent
{
    public LabelComponent? Label { get; init; }
    public string? Id { get; init; }
    public string? Name { get; init; }
    public string? Value { get; init; }
    public string? Type { get; init; }
}
