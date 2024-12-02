using Dfe.Testing.Pages.Components.Inputs;
using Dfe.Testing.Pages.Components.Inputs.Checkbox;
using Dfe.Testing.Pages.Components.Label;
using Dfe.Testing.Pages.Public.Mapper.Abstraction;

namespace Dfe.Testing.Pages.Public.Mapper.GDS;
internal sealed class GDSCheckboxMapper : IComponentMapper<GDSCheckboxComponent>
{
    private readonly ComponentFactory<InputComponent> _inputFactory;
    private readonly ComponentFactory<LabelComponent> _labelFactory;

    public GDSCheckboxMapper(
        ComponentFactory<InputComponent> inputFactory,
        ComponentFactory<LabelComponent> labelFactory)
    {
        _inputFactory = inputFactory;
        _labelFactory = labelFactory;
    }

    public GDSCheckboxComponent Map(IDocumentPart input)
    {
        var inputComponent = _inputFactory.GetManyFromPart(input).Single();
        return new GDSCheckboxComponent()
        {
            Label = _labelFactory.GetManyFromPart(input).Single(),
            Name = inputComponent.Name,
            Value = inputComponent.Value,
            Checked = inputComponent.IsChecked,
        };
    }
}
