namespace Dfe.Testing.Pages.Public.Components;

public record CreatedComponentResponse<T> where T : class
{
    public required T Created { get; init; }
    // TODO needs to be a different external model than IMappingResult?
    public IReadOnlyList<IMappingResult> CreatingComponentResults { get; set; } = [];
}
