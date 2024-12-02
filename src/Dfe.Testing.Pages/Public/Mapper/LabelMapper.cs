using Dfe.Testing.Pages.Components.Label;
using Dfe.Testing.Pages.Public.Mapper.Abstraction;
using Dfe.Testing.Pages.Public.Selector.Factory;

namespace Dfe.Testing.Pages.Public.Mapper;
internal sealed class LabelMapper : IComponentMapper<LabelComponent>
{
    private readonly IComponentSelectorFactory _componentDefaultSelectorFactory;

    public LabelMapper(IComponentSelectorFactory componentDefaultSelectorFactory)
    {
        _componentDefaultSelectorFactory = componentDefaultSelectorFactory;
    }
    public LabelComponent Map(IDocumentPart input)
    {
        var part = input.TagName == "label" ? input : input?.GetChild(_componentDefaultSelectorFactory.GetSelector<LabelComponent>());

        return new()
        {
            For = part?.GetAttribute("for") ?? string.Empty,
            Text = part?.Text ?? string.Empty
        };
    }
}
