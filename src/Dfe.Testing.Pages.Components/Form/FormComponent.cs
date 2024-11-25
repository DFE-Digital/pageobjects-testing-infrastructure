using Dfe.Testing.Pages.Components.Button;
using Dfe.Testing.Pages.Components.Fieldset;
using Dfe.Testing.Pages.Public.DocumentQueryClient;
using HttpMethod = System.Net.Http.HttpMethod;

namespace Dfe.Testing.Pages.Components.Form;

public record FormComponent : IComponent
{
    public required string TagName { get; init; }
    public required IEnumerable<GDSFieldsetComponent> FieldSets { get; init; } = [];
    public required IEnumerable<GDSButtonComponent> Buttons { get; init; } = [];
    public required HttpMethod Method { get; init; }
    public required string Action { get; init; }
    public required bool IsFormValidatedWithHTML { get; init; }
}
