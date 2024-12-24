using Dfe.Testing.Pages.Public.Components.Core.Inputs;
using Dfe.Testing.Pages.Public.Components.Core.Label;
using Dfe.Testing.Pages.Public.Components.GDS.ErrorMessage;

namespace Dfe.Testing.Pages.Public.Components.GDS.TextInput;
public record GDSTextInputComponent
{
    public required LabelComponent Label { get; init; }
    public required TextInputComponent Input { get; init; }
    public GDSErrorMessageComponent ErrorMessage { get; init; } = new() { ErrorMessage = new() { Text = string.Empty } };
}
