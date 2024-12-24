namespace Dfe.Testing.Pages.Internal.DocumentClient.Provider.GetTextHandler.Handlers;
public class DefaultToEmptyTextProcessorHandler : BaseGetTextQueryHandler, IGetTextProcessingHandler
{
    public string Handle(string text)
    {
        var output = text ?? string.Empty;
        return RunNext(output);
    }
}
