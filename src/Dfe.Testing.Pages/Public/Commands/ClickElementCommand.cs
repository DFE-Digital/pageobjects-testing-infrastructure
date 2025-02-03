namespace Dfe.Testing.Pages.Public.Commands;
public class ClickElementCommand : ICommand
{
    public IElementSelector Selector { get; set; } = null!;
    public IElementSelector? FindInScope { get; set; }
}
