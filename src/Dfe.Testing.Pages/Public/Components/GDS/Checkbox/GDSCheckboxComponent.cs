using Dfe.Testing.Pages.Public.Components.Core.Label;
using Dfe.Testing.Pages.Public.Components.GDS.ErrorMessage;

namespace Dfe.Testing.Pages.Public.Components.GDS.Checkbox;

public sealed record GDSCheckboxComponent
{
    public required string Name { get; init; }
    public required string Value { get; init; }
    public required LabelComponent Label { get; init; }
    public bool Checked { get; init; } = false;
    public string Type { get; init; } = "checkbox";
    public GDSErrorMessageComponent ErrorMessage { get; init; } = new() { ErrorMessage = new() { Text = string.Empty } };
    public bool IsRequired { get; init; } = false;
}
