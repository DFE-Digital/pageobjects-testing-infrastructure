using Dfe.Testing.Pages.Components.Checkbox;
using Dfe.Testing.Pages.Components.Fieldset;
using Dfe.Testing.Pages.Components.TextInput;
using Dfe.Testing.Pages.Public.Mapper.Interface;

namespace Dfe.Testing.Pages.Public.Mapper;
internal sealed class GDSFieldsetMapper : IComponentMapper<GDSFieldsetComponent>
{
    private readonly ComponentFactory<GDSCheckboxComponent> _checkboxFactory;
    private readonly ComponentFactory<GDSTextInputComponent> _textInputFactory;

    public GDSFieldsetMapper(
        IDocumentQueryClientAccessor accessor,
        ComponentFactory<GDSCheckboxComponent> checkboxFactory,
        ComponentFactory<GDSTextInputComponent> textInput)
    {
        ArgumentNullException.ThrowIfNull(accessor);
        ArgumentNullException.ThrowIfNull(checkboxFactory);
        ArgumentNullException.ThrowIfNull(textInput);
        _checkboxFactory = checkboxFactory;
        _textInputFactory = textInput;
    }
    public GDSFieldsetComponent Map(IDocumentPart input)
    {
        return new GDSFieldsetComponent()
        {
            TagName = input.TagName,
            Legend = input.GetChild(new CssSelector("legend"))?.Text ?? throw new ArgumentNullException("legend on fieldset is null"),
            TextInputs = _textInputFactory.GetManyFromPart(input),
            Checkboxes = _checkboxFactory.GetManyFromPart(input)
        };
    }
}
