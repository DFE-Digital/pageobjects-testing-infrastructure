namespace Dfe.Testing.Pages.Public.Templates;
public record ButtonComponent
{
    public required string Text { get; init; }
    public ButtonStyleType ButtonStyle { get; init; } = ButtonStyleType.Primary;
    public bool IsEnabled { get; init; } = true;
    public string? Type { get; init; } = "submit";
    public string? Value { get; init; }
    public string? Name { get; init; }
}

public enum ButtonStyleType
{
    Primary,
    Secondary,
    Warning
}
