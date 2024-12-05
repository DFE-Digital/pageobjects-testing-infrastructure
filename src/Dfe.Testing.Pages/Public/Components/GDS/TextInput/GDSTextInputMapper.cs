using Dfe.Testing.Pages.Public.Components.GDS.ErrorMessage;
using Dfe.Testing.Pages.Public.Components.Inputs;
using Dfe.Testing.Pages.Public.Components.Label;

namespace Dfe.Testing.Pages.Public.Components.GDS.Inputs.TextInput;
internal sealed class GDSTextInputMapper : IComponentMapper<GDSTextInputComponent>
{
    private readonly ComponentFactory<TextInputComponent> _textInputFactory;
    private readonly ComponentFactory<LabelComponent> _labelFactory;
    private readonly ComponentFactory<GDSErrorMessageComponent> _errorMessageFactory;

    public GDSTextInputMapper(
        ComponentFactory<TextInputComponent> inputFactory,
        ComponentFactory<LabelComponent> labelFactory,
        ComponentFactory<GDSErrorMessageComponent> errorMessageFactory)
    {
        ArgumentNullException.ThrowIfNull(inputFactory);
        ArgumentNullException.ThrowIfNull(labelFactory);
        ArgumentNullException.ThrowIfNull(errorMessageFactory);
        _textInputFactory = inputFactory;
        _labelFactory = labelFactory;
        _errorMessageFactory = errorMessageFactory;
    }
    public GDSTextInputComponent Map(IDocumentPart input)
    {
        var label = _labelFactory.GetManyFromPart(input).Single();
        var textInput = _textInputFactory.GetManyFromPart(input).Single();

        return new GDSTextInputComponent()
        {
            Label = label,
            Input = textInput,
            ErrorMessage = _errorMessageFactory.GetManyFromPart(input).SingleOrDefault() ?? new GDSErrorMessageComponent() { ErrorMessage = string.Empty }
        };
    }
}
