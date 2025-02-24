using Dfe.Testing.Pages.Internal.DocumentClient.Options;
using Dfe.Testing.Pages.Internal.DocumentClient.Provider.GetTextHandler;
using Dfe.Testing.Pages.Internal.DocumentClient.Provider.GetTextHandler.Factory;
using Dfe.Testing.Pages.Internal.DocumentClient.Provider.GetTextHandler.Options;
using Dfe.Testing.Pages.Public.PageObjects;

namespace Dfe.Testing.Pages.Internal.DocumentClient.Provider;
internal abstract class BaseDocumentClientProvider
{
    protected readonly DocumentClientOptions _documentClientOptions;
    private readonly IMapper<DocumentClientOptions, TextProcessingOptions> _mapper;
    private readonly IGetTextProcessingHandlerFactory _getTextProcessingHandlerFactory;

    protected BaseDocumentClientProvider(
        // TODO facade so injected depdencies into each provider is slimmed down
        IGetTextProcessingHandlerFactory getTextProcessingHandlerFactory,
        IMapper<DocumentClientOptions, TextProcessingOptions> mapper,
        DocumentClientOptions documentClientOptions)
    {
        _getTextProcessingHandlerFactory = getTextProcessingHandlerFactory;
        _mapper = mapper;
        _documentClientOptions = documentClientOptions;
    }

    protected IGetTextProcessingHandler GetTextProcessingHandler()
        => _getTextProcessingHandlerFactory.Create(
                _mapper.Map(_documentClientOptions));
}
