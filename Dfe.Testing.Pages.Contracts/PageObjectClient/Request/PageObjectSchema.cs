namespace Dfe.Testing.Pages.Contracts.PageObjectClient.Request;
public record PageObjectSchema
{
    public string Id { get; set; } = string.Empty;
    public string Find { get; set; } = string.Empty;
    public IEnumerable<PageObjectSchema> Children { get; set; } = [];
}
