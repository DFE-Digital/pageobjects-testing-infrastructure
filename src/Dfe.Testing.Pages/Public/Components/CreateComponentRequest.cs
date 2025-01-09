namespace Dfe.Testing.Pages.Public.Components;
public sealed class CreateComponentRequest
{
    /// <summary>
    /// Optional Selector to find the component in the document
    /// if not provided, a defaulted lookup for the component is attempted from <see cref="SelectorFactory.IComponentSelectorFactory"/>
    /// </summary>
    public IElementSelector? Selector { get; set; }

    /// <summary>
    /// Optional Selector to find the component in a part of the document. 
    /// If not provided, the request is executed over the entire document
    /// </summary>
    public IElementSelector? FindInScope { get; set; }
}
