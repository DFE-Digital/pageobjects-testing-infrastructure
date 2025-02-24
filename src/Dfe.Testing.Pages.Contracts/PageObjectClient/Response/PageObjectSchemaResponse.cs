namespace Dfe.Testing.Pages.Contracts.PageObjectClient.Response;
public record PageObjectSchemaResponse
{
    public required MappingStatus Status { get; init; }
    public string Message { get; init; } = string.Empty;
}

public enum MappingStatus
{
    Success,
    QueryFailed,
    MappingFailed
}
