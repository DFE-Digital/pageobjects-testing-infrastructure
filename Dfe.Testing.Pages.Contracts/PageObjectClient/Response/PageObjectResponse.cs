namespace Dfe.Testing.Pages.Contracts.PageObjectClient.Response;
public record PageObjectResponse
{
    public IReadOnlyList<CreatedPageObjectModel> Created { get; set; } = [];
}
