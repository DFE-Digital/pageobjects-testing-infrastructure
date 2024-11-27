using Dfe.Testing.Pages.Components.Label;
using Dfe.Testing.Pages.Public.Mapper.Abstraction;

namespace Dfe.Testing.Pages.Public.Mapper;
internal sealed class LabelMapper : IComponentMapper<LabelComponent>
{
    private readonly IComponentDefaultSelectorFactory _componentDefaultSelectorFactory;

    public LabelMapper(IComponentDefaultSelectorFactory componentDefaultSelectorFactory)
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
