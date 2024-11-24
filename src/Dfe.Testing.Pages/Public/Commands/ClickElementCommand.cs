namespace Dfe.Testing.Pages.Public.Commands;
public class ClickElementCommand : ICommand
{
    public required IElementSelector FindWith { get; set; }
    public IElementSelector? InScope { get; set; }
}
