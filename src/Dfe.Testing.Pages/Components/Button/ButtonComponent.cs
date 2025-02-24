using Dfe.Testing.Pages.Public.PageObjects;
using Dfe.Testing.Pages.Public.PageObjects.PageObjectClient.Response;

namespace Dfe.Testing.Pages.Components.Button;
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

public sealed class ButtonComponentMapper : IMapper<CreatedPageObjectModel, ButtonComponent>
{
    public ButtonComponent Map(CreatedPageObjectModel input)
    {
        return new ButtonComponent()
        {
            Text = input.GetAttribute("text") ?? string.Empty,
            Type = input.GetAttribute("type"),
            Name = input.GetAttribute("name"),
            Value = input.GetAttribute("value"),
            IsEnabled = input.GetAttribute("disabled") is null,
            ButtonStyle = input.GetAttribute("class") switch
            {
                null => ButtonStyleType.Primary,
                var s when s.Contains("govuk-button--secondary") => ButtonStyleType.Secondary,
                var s when s.Contains("govuk-button--warning") => ButtonStyleType.Warning,
                _ => ButtonStyleType.Primary
            }
        };
    }
}
