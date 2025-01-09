using Dfe.Testing.Pages.Public.Components.GDS.ErrorMessage;
using Dfe.Testing.Pages.Public.Components.Inputs;
using Dfe.Testing.Pages.Public.Components.Label;

namespace Dfe.Testing.Pages.Public.Components.GDS.TextInput;
public record GDSTextInputComponent
{
    public required LabelComponent? Label { get; init; }
    public required TextInputComponent? TextInput { get; init; }
    public required GDSErrorMessageComponent? ErrorMessage { get; init; }
}
