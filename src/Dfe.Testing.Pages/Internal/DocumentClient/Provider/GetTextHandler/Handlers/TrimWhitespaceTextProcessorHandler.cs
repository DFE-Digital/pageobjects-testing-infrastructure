namespace Dfe.Testing.Pages.Internal.DocumentClient.Provider.GetTextHandler.Handlers;
public class TrimWhitespaceTextProcessorHandler : BaseGetTextQueryHandler, IGetTextProcessingHandler
{
    public string Handle(string text)
    {
        var output = text.Trim();
        return RunNext(output);
    }
}
