using HttpMethod = System.Net.Http.HttpMethod;

namespace Dfe.Testing.Pages.Public.Templates;
public record FormComponent
{
    public HttpMethod? Method { get; init; }
    public string? Action { get; init; }
    public IEnumerable<ButtonComponent> Buttons { get; init; } = [];
    public IEnumerable<InputComponent> Inputs { get; init; } = [];
}
