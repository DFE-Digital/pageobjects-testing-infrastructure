namespace Dfe.Testing.Pages.Public.Commands;
public interface ICommand
{
    public IElementSelector FindWith { get; set; }
    public IElementSelector? InScope { get; set; }
}
