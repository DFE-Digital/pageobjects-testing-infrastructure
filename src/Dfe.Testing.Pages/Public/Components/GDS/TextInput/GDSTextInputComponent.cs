using Dfe.Testing.Pages.Public.Components.GDS.ErrorMessage;
using Dfe.Testing.Pages.Public.Components.Label;
using Dfe.Testing.Pages.Public.Components.TextInput;

namespace Dfe.Testing.Pages.Public.Components.GDS.TextInput;
public record GDSTextInputComponent
{
    public required LabelComponent? Label { get; init; }
    public required TextInputComponent? TextInput { get; init; }
    public required GDSErrorMessageComponent? ErrorMessage { get; init; }
}
