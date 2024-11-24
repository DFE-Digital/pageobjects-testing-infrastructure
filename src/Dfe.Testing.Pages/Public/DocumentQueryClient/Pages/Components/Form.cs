using HttpMethod = System.Net.Http.HttpMethod;

namespace Dfe.Testing.Pages.Public.DocumentQueryClient.Pages.Components;

public record Form : IComponent
{
    public required string TagName { get; init; }
    public required IEnumerable<GDSFieldset> FieldSets { get; init; } = [];
    public required IEnumerable<GDSButton> Buttons { get; init; } = [];
    public required HttpMethod Method { get; init; }
    public required string Action { get; init; }
    public required bool IsFormValidatedWithHTML { get; init; }
}
