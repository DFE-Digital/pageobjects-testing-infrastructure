using Dfe.Testing.Pages.Internal.DocumentClient.Provider.GetTextHandler.Options;

namespace Dfe.Testing.Pages.Internal.DocumentClient.Provider.GetTextHandler.Factory;
public interface IGetTextProcessingHandlerFactory
{
    public IGetTextProcessingHandler Create(TextProcessingOptions textProcessingOptions);
}
