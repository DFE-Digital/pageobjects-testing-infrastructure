namespace Dfe.Testing.Pages.Public.Components;

public record CreatedComponentResponse<T> where T : class
{
    public required T Created { get; init; }
    public IReadOnlyList<IMappingResult> CreatingComponentResults { get; set; } = [];
}
