namespace Dfe.Testing.Pages.Public;
public sealed class UpdateElementTextCommand : ICommand
{
    public IElementSelector Selector { get; set; } = null!;
    public IElementSelector? FindInScope { get; set; }
    public string Text { get; set; } = string.Empty;
}
