using Dfe.Testing.Pages.Components.Input;
using Dfe.Testing.Pages.Components.Inputs.TextInput;
using Dfe.Testing.Pages.Components.Label;
using Dfe.Testing.Pages.Public.Mapper.Abstraction;

namespace Dfe.Testing.Pages.Public.Mapper.GDS;
internal class GDSTextInputMapper : IComponentMapper<GDSTextInputComponent>
{
    private readonly ComponentFactory<InputComponent> _inputFactory;
    private readonly ComponentFactory<LabelComponent> _labelFactory;

    public GDSTextInputMapper(
        ComponentFactory<InputComponent> inputFactory,
        ComponentFactory<LabelComponent> labelFactory)
    {
        ArgumentNullException.ThrowIfNull(inputFactory);
        ArgumentNullException.ThrowIfNull(labelFactory);
        _inputFactory = inputFactory;
        _labelFactory = labelFactory;
    }
    public GDSTextInputComponent Map(IDocumentPart input)
    {
        var textInput = _inputFactory.GetFromPart(input);
        return new GDSTextInputComponent()
        {
            Label = _labelFactory.GetFromPart(input),
            Value = textInput.Value,
            Name = textInput.Name,
            Type = textInput.Type,
            PlaceHolder = textInput.PlaceHolder,
            IsRequired = textInput.IsRequired,
        };
    }
}
