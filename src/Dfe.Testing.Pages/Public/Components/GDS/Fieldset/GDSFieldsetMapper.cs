using Dfe.Testing.Pages.Public.Components.GDS.Checkbox;
using Dfe.Testing.Pages.Public.Components.GDS.Inputs.TextInput;
using Dfe.Testing.Pages.Public.Components.GDS.Radio;

namespace Dfe.Testing.Pages.Public.Components.GDS.Fieldset;
internal sealed class GDSFieldsetMapper : IComponentMapper<GDSFieldsetComponent>
{
    private readonly ComponentFactory<GDSCheckboxComponent> _checkboxFactory;
    private readonly ComponentFactory<GDSRadioComponent> _radioFactory;
    private readonly ComponentFactory<GDSTextInputComponent> _textInputFactory;
    private readonly ComponentFactory<GDSSelectComponent> _selectComponentFactory;

    public GDSFieldsetMapper(
        ComponentFactory<GDSCheckboxComponent> checkboxFactory,
        ComponentFactory<GDSRadioComponent> radioFactory,
        ComponentFactory<GDSTextInputComponent> textInput,
        ComponentFactory<GDSSelectComponent> selectComponentFactory)
    {
        ArgumentNullException.ThrowIfNull(checkboxFactory);
        ArgumentNullException.ThrowIfNull(textInput);
        ArgumentNullException.ThrowIfNull(radioFactory);
        ArgumentNullException.ThrowIfNull(selectComponentFactory);
        _checkboxFactory = checkboxFactory;
        _radioFactory = radioFactory;
        _textInputFactory = textInput;
        _selectComponentFactory = selectComponentFactory;
    }
    public GDSFieldsetComponent Map(IDocumentPart input)
    {
        return new GDSFieldsetComponent()
        {
            TagName = input.TagName ?? "legend",
            Legend = input.FindDescendant(new CssElementSelector("legend"))?.Text ?? throw new ArgumentNullException("legend on fieldset is null"),
            TextInputs = _textInputFactory.GetManyFromPart(input),
            Radios = _radioFactory.GetManyFromPart(input),
            Checkboxes = _checkboxFactory.GetManyFromPart(input)
        };
    }
}
