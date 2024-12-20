﻿using Dfe.Testing.Pages.Public.Components.GDS.Button;
using Dfe.Testing.Pages.Public.Components.GDS.Checkbox;
using Dfe.Testing.Pages.Public.Components.GDS.Fieldset;
using Dfe.Testing.Pages.Public.Components.GDS.Inputs.TextInput;
using Dfe.Testing.Pages.Public.Components.GDS.Radio;
using Dfe.Testing.Pages.Public.Components.Inputs;
using HttpMethod = System.Net.Http.HttpMethod;

namespace Dfe.Testing.Pages.Public.Components.Form;
internal sealed class FormMapper : IComponentMapper<FormComponent>
{
    private readonly ComponentFactory<GDSFieldsetComponent> _fieldSetFactory;
    private readonly ComponentFactory<GDSButtonComponent> _buttonFactory;
    private readonly ComponentFactory<GDSTextInputComponent> _textInputFactory;
    private readonly ComponentFactory<GDSCheckboxComponent> _checkboxFactory;
    private readonly ComponentFactory<GDSRadioComponent> _radioFactory;
    private readonly ComponentFactory<GDSSelectComponent> _selectFactory;
    private readonly ComponentFactory<HiddenInputComponent> _hiddenInputFactory;

    public FormMapper(
        ComponentFactory<GDSFieldsetComponent> fieldSetFactory,
        ComponentFactory<GDSButtonComponent> buttonFactory,
        ComponentFactory<GDSTextInputComponent> textInputFactory,
        ComponentFactory<GDSCheckboxComponent> checkboxFactory,
        ComponentFactory<GDSRadioComponent> radioFactory,
        ComponentFactory<GDSSelectComponent> selectFactory,
        ComponentFactory<HiddenInputComponent> hiddenInputFactory)
    {
        ArgumentNullException.ThrowIfNull(fieldSetFactory);
        ArgumentNullException.ThrowIfNull(buttonFactory);
        ArgumentNullException.ThrowIfNull(textInputFactory);
        ArgumentNullException.ThrowIfNull(checkboxFactory);
        ArgumentNullException.ThrowIfNull(radioFactory);
        ArgumentNullException.ThrowIfNull(selectFactory);
        ArgumentNullException.ThrowIfNull(hiddenInputFactory);
        _fieldSetFactory = fieldSetFactory;
        _buttonFactory = buttonFactory;
        _textInputFactory = textInputFactory;
        _checkboxFactory = checkboxFactory;
        _radioFactory = radioFactory;
        _selectFactory = selectFactory;
        _hiddenInputFactory = hiddenInputFactory;
    }
    public FormComponent Map(IDocumentPart input)
    {
        return new FormComponent()
        {
            Method = HttpMethod.Parse(
                input.GetAttribute("method") ?? throw new ArgumentNullException(nameof(FormComponent.Method), "method on form is null")),
            Action = input.GetAttribute("action") ?? string.Empty,
            IsFormValidated = !input.HasAttribute("novalidate"),
            FieldSets = _fieldSetFactory.GetManyFromPart(input) ?? [],
            Buttons = _buttonFactory.GetManyFromPart(input) ?? [],
            HiddenInputs = _hiddenInputFactory.GetManyFromPart(input) ?? [],
            TextInputs = _textInputFactory.GetManyFromPart(input) ?? [],
            Checkboxes = _checkboxFactory.GetManyFromPart(input) ?? [],
            Radios = _radioFactory.GetManyFromPart(input) ?? [],
            Selects = _selectFactory.GetManyFromPart(input) ?? []
        };
    }
}
