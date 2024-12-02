using Dfe.Testing.Pages.Components.Label;
using Dfe.Testing.Pages.Components.Select;
using Dfe.Testing.Pages.Public.Mapper.Abstraction;

namespace Dfe.Testing.Pages.Public.Mapper.GDS;
internal sealed class GDSSelectMapper : IComponentMapper<GDSSelectComponent>
{
    private readonly ComponentFactory<LabelComponent> _labelFactory;
    private readonly ComponentFactory<OptionComponent> _optionFactory;

    public GDSSelectMapper(
        ComponentFactory<LabelComponent> labelFactory,
        ComponentFactory<OptionComponent> optionFactory)
    {
        _labelFactory = labelFactory;
        _optionFactory = optionFactory;
    }
    public GDSSelectComponent Map(IDocumentPart input)
    {
        return new GDSSelectComponent()
        {
            Label = _labelFactory.GetManyFromPart(input).Single(),
            Options = _optionFactory.GetManyFromPart(input)
        };
    }
}
