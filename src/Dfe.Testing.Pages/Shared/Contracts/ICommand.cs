namespace Dfe.Testing.Pages.Shared.Contracts;
public interface ICommand
{
    public IElementSelector Selector { get; set; }
    public IElementSelector? FindInScope { get; set; }
}
