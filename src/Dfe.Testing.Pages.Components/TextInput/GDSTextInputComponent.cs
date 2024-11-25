using Dfe.Testing.Pages.Public.DocumentQueryClient;

namespace Dfe.Testing.Pages.Components.TextInput;
public record GDSTextInputComponent : IComponent
{
    public required string Name { get; init; }
    public required string Label { get; init; }
    public required string TagName { get; init; }
    public string ErrorMessage { get; init; } = string.Empty;
    public string Hint { get; init; } = string.Empty;
    public string? Type { get; init; } = "text";
    public string? PlaceHolder { get; init; } = null;
    public bool IsNumeric { get; init; } = false;
    public string AutoComplete { get; init; } = string.Empty;
}

// TODO include error message in component
