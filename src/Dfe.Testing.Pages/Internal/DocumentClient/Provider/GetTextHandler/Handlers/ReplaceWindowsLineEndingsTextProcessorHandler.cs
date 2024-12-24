namespace Dfe.Testing.Pages.Internal.DocumentClient.Provider.GetTextHandler.Handlers;
public class ReplaceWindowsLineEndingsTextProcessorHandler : BaseGetTextQueryHandler, IGetTextProcessingHandler
{
    public string Handle(string text)
    {
        var output = text.Replace("\r\n", string.Empty);
        return RunNext(output);
    }
}
