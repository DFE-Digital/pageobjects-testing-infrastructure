using Dfe.Testing.Pages.Components.Inputs;
using Dfe.Testing.Pages.Components.Inputs.Radio;
using Dfe.Testing.Pages.Components.Label;
using Dfe.Testing.Pages.Public.Mapper.Abstraction;

namespace Dfe.Testing.Pages.Public.Mapper.GDS;
internal sealed class GDSRadioMapper : IComponentMapper<GDSRadioComponent>
{
    private readonly ComponentFactory<LabelComponent> _labelFactory;
    private readonly ComponentFactory<InputComponent> _inputFactory;

    public GDSRadioMapper(
        ComponentFactory<LabelComponent> labelFactory,
        ComponentFactory<InputComponent> inputComponent)
    {
        ArgumentNullException.ThrowIfNull(labelFactory);
        _labelFactory = labelFactory;
        _inputFactory = inputComponent;
    }
    public GDSRadioComponent Map(IDocumentPart input)
    {
        var inputComponent = _inputFactory.GetManyFromPart(input).Single();
        return new()
        {
            Label = _labelFactory.GetManyFromPart(input).Single(),
            Name = inputComponent.Name,
            Value = inputComponent.Value,
        };
    }
}
