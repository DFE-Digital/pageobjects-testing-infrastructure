namespace Dfe.Testing.Pages.Contracts.PageObjectClient.Request;
public class PageObjectRequest
{
    public DocumentQueryOptions? Query { get; set; } = null;
    public IEnumerable<PageObjectSchema> MapSchemas { get; set; } = [];
}

public class DocumentQueryOptions
{
    public string Find { get; set; } = string.Empty;
    public string? InScope { get; set; } = null!;
}
