namespace Dfe.Testing.Pages.Public.Components.MappingAbstraction.Response;
internal record MappedResponse<T> where T : class
{
    public T? Mapped { get; init; } = null!;
    public required List<IMappingResult> MappingResults { get; init; }
}

public interface IMappingResult
{
    public string MapKey { get; }
    public MappingStatus Status { get; }
    public string AttemptedToMapFrom { get; }
    public string Message { get; }
}

public enum MappingStatus
{
    Success,
    Failed
}

internal static class MappedResponseExtensions
{
    internal static MappedResponse<T> AddToMappingResults<T>(this MappedResponse<T> response, IEnumerable<IMappingResult> results) where T : class
    {
        response.MappingResults.AddRange(results);
        return response;
    }
}
