using Dfe.Testing.Pages.Internal.DocumentClient.Options;
using Dfe.Testing.Pages.Internal.DocumentClient.Provider.GetTextHandler.Factory;
using Dfe.Testing.Pages.Internal.DocumentClient.Provider.GetTextHandler.Options;

namespace Dfe.Testing.Pages.Internal.DocumentClient.Provider.AngleSharp;
internal sealed class AngleSharpDocumentClientProvider : IDocumentClientProvider
{
    private readonly IHtmlDocumentProvider _documentProvider;
    private readonly DocumentClientOptions _documentQueryClientOptions;
    private readonly IMapper<DocumentClientOptions, TextProcessingOptions> _documentClientOptionsToTextProcessingMapper;
    private readonly IGetTextProcessingHandlerFactory _textProcessingStrategy;

    public AngleSharpDocumentClientProvider(
        IHtmlDocumentProvider documentProvider,
        DocumentClientOptions documentQueryClientOptions,
        IMapper<DocumentClientOptions, TextProcessingOptions> documentQueryClientOptionsToTextProcessingMapper,
        IGetTextProcessingHandlerFactory textProcessingStrategy)
    {
        _documentProvider = documentProvider;
        _documentQueryClientOptions = documentQueryClientOptions;
        _documentClientOptionsToTextProcessingMapper = documentQueryClientOptionsToTextProcessingMapper;
        _textProcessingStrategy = textProcessingStrategy;
    }
    public async Task<IDocumentClient> CreateDocumentClientAsync(HttpRequestMessage httpRequestMessage)
    {
        var document = await _documentProvider.Get(httpRequestMessage);
        var processingOptions = _documentClientOptionsToTextProcessingMapper.Map(_documentQueryClientOptions);
        var textProcessingStrategy = _textProcessingStrategy.Create(processingOptions);

        return new AngleSharpDocumentClient(
            textProcessingStrategy,
            document);
    }
}
