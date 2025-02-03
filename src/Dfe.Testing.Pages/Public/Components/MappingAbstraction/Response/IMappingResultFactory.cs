namespace Dfe.Testing.Pages.Public.Components.MappingAbstraction.Response;
public interface IMappingResultFactory
{
    MappedResponse<TOut> Create<TOut>(
        TOut? mapped,
        MappingStatus status,
        IDocumentSection section,
        string message = "") where TOut : class;
}

internal sealed class MappingResultFactory : IMappingResultFactory
{
    public MappedResponse<TOut> Create<TOut>(
        TOut? mapped,
        MappingStatus status,
        IDocumentSection section,
        string message = "") where TOut : class
    {
        ArgumentNullException.ThrowIfNull(section);
        IMappingResult result = status switch
        {
            MappingStatus.Failed => new FailedMappingResultFromDocumentSection(section, message),
            MappingStatus.Success => new SuccessfulMappingResultFromDocumentSection(section),
            _ => throw new ArgumentOutOfRangeException(nameof(status), status, "Unknown mapping status")
        };

        return new MappedResponse<TOut>
        {
            Mapped = mapped,
            MappingResult = result
        };
    }

    internal abstract class DocumentSectionMappingResult : IMappingResult
    {
        public DocumentSectionMappingResult(IDocumentSection section, MappingStatus status)
        {
            ArgumentNullException.ThrowIfNull(section);
            AttemptedToMapFrom = section.Document;
            Status = status;
        }
        public MappingStatus Status { get; init; }
        public string AttemptedToMapFrom { get; init; } = string.Empty;
        public string Message { get; init; } = string.Empty;
    }


    internal sealed class SuccessfulMappingResultFromDocumentSection : DocumentSectionMappingResult
    {
        public SuccessfulMappingResultFromDocumentSection(IDocumentSection section) : base(section, MappingStatus.Success)
        {
        }
    }

    internal sealed class FailedMappingResultFromDocumentSection : DocumentSectionMappingResult
    {
        public FailedMappingResultFromDocumentSection(IDocumentSection section, string message) : base(section, MappingStatus.Failed)
        {
            ArgumentException.ThrowIfNullOrEmpty(message);
            Message = message ?? string.Empty;
        }
    }
}
