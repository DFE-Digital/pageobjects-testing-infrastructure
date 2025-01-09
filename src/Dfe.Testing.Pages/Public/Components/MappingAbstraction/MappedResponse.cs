namespace Dfe.Testing.Pages.Public.Components.MappingAbstraction;
public record MappedResponse<T> where T : class
{
    public T? Mapped { get; init; } = null!;
    public required IMappingResult MappingResult { get; init; }
}

public interface IMappingResult
{
    // TODO how do we uniquely identify and set MappingResult information {FactoryCreateNumber}.{ChildComponent}.{Attribute}? public string Key { get; }
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
    internal static IEnumerable<MappedResponse<T>> AddMappedResponseToResults<T>(this IEnumerable<MappedResponse<T>> responses, IList<IMappingResult> results) where T : class
    {
        ArgumentNullException.ThrowIfNull(responses);
        ArgumentNullException.ThrowIfNull(results);
        responses.ToList().ForEach((mappedResponse) => results.Add(mappedResponse.MappingResult));
        return responses;
    }

    internal static MappedResponse<T> AddMappedResponseToResults<T>(this MappedResponse<T> response, IList<IMappingResult> results) where T : class
        => AddMappedResponseToResults(new List<MappedResponse<T>> { response }, results).Single();
}
