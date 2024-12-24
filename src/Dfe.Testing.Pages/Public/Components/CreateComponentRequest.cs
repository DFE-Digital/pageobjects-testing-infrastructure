namespace Dfe.Testing.Pages.Public.Components;
public sealed class CreateComponentRequest
{
    public IElementSelector? Selector { get; set; }
    public IElementSelector? FindInScope { get; set; }
}
