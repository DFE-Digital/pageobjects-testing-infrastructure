namespace Dfe.Testing.Pages.Public.Components;

// TODO consider a builder in front of the CreateComponentRequest so the type is hidden
public sealed class CreateComponentRequest
{
    /// <summary>
    /// Optional Selector to find the component in the document
    /// if not provided, a defaulted lookup for the component is attempted from <see cref="EntrypointSelectorFactory.IEntrypointSelectorFactory"/>
    /// </summary>
    public IElementSelector? Selector { get; set; }

    /// <summary>
    /// Optional Selector to find the component in a part of the document. 
    /// If not provided, the request is executed over the entire document
    /// </summary>
    public IElementSelector? FindInScope { get; set; }
    /// <summary>
    /// Optional override the default MapEntrypoint for any part of the component, or its descendants
    /// </summary>
    public Dictionary<string, IElementSelector> Mapping { get; set; } = [];
}
