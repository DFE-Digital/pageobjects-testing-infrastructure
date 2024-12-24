namespace Dfe.Testing.Pages.Internal.DocumentClient.Provider.GetTextHandler.Handlers;
public abstract class BaseGetTextQueryHandler
{
    protected IGetTextProcessingHandler? _next;
    public void SetNext(IGetTextProcessingHandler next)
    {
        ArgumentNullException.ThrowIfNull(next);
        _next = next;
    }

    protected string RunNext(string text) => _next == null ? text : _next.Handle(text);
}
