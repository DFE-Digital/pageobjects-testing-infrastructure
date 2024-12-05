namespace Dfe.Testing.Pages.Public.Components.GDS.ErrorMessage;
public record GDSErrorMessageComponent : IComponent
{
    public required string ErrorMessage { get; init; }
}
