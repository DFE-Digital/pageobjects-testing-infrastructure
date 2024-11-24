
namespace Dfe.Testing.Pages.Public.Commands;
public sealed class UpdateElementTextCommand : ICommand
{
    public required string Text { get; set; } = string.Empty;
    public required IElementSelector FindWith { get; set; }
    public IElementSelector? InScope { get; set; }
}
