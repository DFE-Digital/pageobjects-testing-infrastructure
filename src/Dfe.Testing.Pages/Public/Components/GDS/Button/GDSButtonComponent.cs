using Dfe.Testing.Pages.Public.Components.Text;

namespace Dfe.Testing.Pages.Public.Components.GDS.Button;

public record GDSButtonComponent
{
    internal GDSButtonComponent() { }
    public required ButtonStyleType ButtonStyle { get; init; } = ButtonStyleType.Primary;
    public required TextComponent Text { get; init; }
    public bool IsEnabled { get; init; } = false;
    public string Type { get; init; } = "submit";
    public string Value { get; init; } = string.Empty;
    public string Name { get; init; } = string.Empty;
}

public enum ButtonStyleType
{
    Primary,
    Secondary,
    Warning
}

public interface IGDSButtonBuilder
{
    GDSButtonComponent Build();
    IGDSButtonBuilder SetButtonStyle(ButtonStyleType buttonType);
    IGDSButtonBuilder SetEnabled(bool enabled);
    IGDSButtonBuilder SetType(string type);
    IGDSButtonBuilder SetName(string name);
    IGDSButtonBuilder SetText(string text);
    IGDSButtonBuilder SetValue(string value);
}
