namespace Dfe.Testing.Pages.Public.PageObjects.PageObjectClient.Response;
public record PageObjectResponse
{
    public IReadOnlyList<CreatedPageObjectModel> Created { get; set; } = [];
}
