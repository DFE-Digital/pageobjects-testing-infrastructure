namespace Dfe.Testing.Pages.Public.Components.MappingAbstraction.Response;
internal interface IMappingResultFactory
{
    MappedResponse<TOut> Create<TOut>(
        MapKey key,
        TOut? mapped,
        MappingStatus status,
        IDocumentSection section,
        string message = "") where TOut : class;
}

internal sealed class MappingResultFactory : IMappingResultFactory
{
    public MappedResponse<TOut> Create<TOut>(
        MapKey key,
        TOut? mapped,
        MappingStatus status,
        IDocumentSection section,
        string message = "") where TOut : class
    {
        ArgumentNullException.ThrowIfNull(section);
        IMappingResult result = status switch
        {
            MappingStatus.Failed => new FailedMappingResultFromDocumentSection(key, section, message),
            MappingStatus.Success => new SuccessfulMappingResultFromDocumentSection(key, section),
            _ => throw new ArgumentOutOfRangeException(nameof(status), status, "Unknown mapping status")
        };

        return new MappedResponse<TOut>
        {
            Mapped = mapped,
            MappingResults = [result]
        };
    }

    internal abstract class DocumentSectionMappingResult : IMappingResult
    {
        public DocumentSectionMappingResult(MapKey key, IDocumentSection section, MappingStatus status)
        {
            ArgumentNullException.ThrowIfNull(key);
            ArgumentNullException.ThrowIfNull(section);
            AttemptedToMapFrom = section.Document;
            Status = status;
            MapKey = key.ToString();
        }
        public string MapKey { get; init; } = string.Empty;
        public MappingStatus Status { get; init; }
        public string AttemptedToMapFrom { get; init; } = string.Empty;
        public string Message { get; init; } = string.Empty;
    }


    internal sealed class SuccessfulMappingResultFromDocumentSection : DocumentSectionMappingResult
    {
        public SuccessfulMappingResultFromDocumentSection(MapKey key, IDocumentSection section) : base(key, section, MappingStatus.Success)
        {
        }
    }

    internal sealed class FailedMappingResultFromDocumentSection : DocumentSectionMappingResult
    {
        public FailedMappingResultFromDocumentSection(MapKey key, IDocumentSection section, string message) : base(key, section, MappingStatus.Failed)
        {
            ArgumentException.ThrowIfNullOrEmpty(message);
            Message = message ?? string.Empty;
        }
    }
}
