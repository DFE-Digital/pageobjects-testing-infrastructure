namespace Dfe.Testing.Pages.Public.Commands;
public interface ICommand
{
    public IElementSelector Selector { get; set; }
    public IElementSelector? FindInScope { get; set; }
}
