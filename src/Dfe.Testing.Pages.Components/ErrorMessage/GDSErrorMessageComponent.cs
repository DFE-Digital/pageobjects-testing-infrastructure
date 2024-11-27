namespace Dfe.Testing.Pages.Components.ErrorMessage;
public record GDSErrorMessageComponent : IComponent
{
    public required string ErrorMessage { get; init; }
}
