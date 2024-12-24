using Dfe.Testing.Pages.Internal.DocumentClient.Provider.GetTextHandler.Handlers;
using Dfe.Testing.Pages.Internal.DocumentClient.Provider.GetTextHandler.Options;

namespace Dfe.Testing.Pages.Internal.DocumentClient.Provider.GetTextHandler.Factory;
public sealed class GetTextProcessingHandlerFactory : IGetTextProcessingHandlerFactory
{
    public IGetTextProcessingHandler Create(TextProcessingOptions textProcessingOptions)
    {
        // entry handler added first
        List<BaseGetTextQueryHandler> handlers = [new DefaultToEmptyTextProcessorHandler()];

        // TODO generic replacer with replace regex passed?
        SetNextHandler(new ReplaceUnixLineEndingsTextProcessorHandler());
        SetNextHandler(new ReplaceWindowsLineEndingsTextProcessorHandler());

        if (textProcessingOptions.Trim)
        {
            SetNextHandler(new TrimWhitespaceTextProcessorHandler());
        }

        return (IGetTextProcessingHandler)handlers.First();

        void SetNextHandler(BaseGetTextQueryHandler handler)
        {
            handlers.Last().SetNext((IGetTextProcessingHandler)handler);
            handlers.Add(handler);
        }
    }
}
