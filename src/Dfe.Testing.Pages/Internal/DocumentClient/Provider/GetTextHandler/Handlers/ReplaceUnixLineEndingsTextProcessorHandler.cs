namespace Dfe.Testing.Pages.Internal.DocumentClient.Provider.GetTextHandler.Handlers;
public class ReplaceUnixLineEndingsTextProcessorHandler : BaseGetTextQueryHandler, IGetTextProcessingHandler
{
    public string Handle(string text)
    {
        var output = text.Replace("\n", string.Empty);
        return RunNext(output);
    }
}
