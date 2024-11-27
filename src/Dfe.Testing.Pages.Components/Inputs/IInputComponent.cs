using Dfe.Testing.Pages.Components.ErrorMessage;

namespace Dfe.Testing.Pages.Components.Inputs;
internal interface IInputComponent : IComponent
{
    string Name { get; init; }
    string Value { get; init; }
    string Type { get; init; }
    bool IsRequired { get; init; }
    GDSErrorMessageComponent ErrorMessage { get; init; }
}
