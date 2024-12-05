using Dfe.Testing.Pages.Public.Components.GDS.ErrorMessage;
using Dfe.Testing.Pages.Public.Components.Label;

namespace Dfe.Testing.Pages.Public.Components.GDS.Checkbox;

public sealed record GDSCheckboxComponent : IInputComponent
{
    public required string Name { get; init; }
    public required string Value { get; init; }
    public required LabelComponent Label { get; init; }
    public bool Checked { get; init; } = false;
    public string Type { get; init; } = "checkbox";
    public GDSErrorMessageComponent ErrorMessage { get; init; } = new() { ErrorMessage = string.Empty };
    public bool IsRequired { get; init; } = false;
}

internal interface IInputComponent : IComponent
{
    string Name { get; init; }
    string Value { get; init; }
    string Type { get; init; }
    bool IsRequired { get; init; }
    GDSErrorMessageComponent ErrorMessage { get; init; }
}
