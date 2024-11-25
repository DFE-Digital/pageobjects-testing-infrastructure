using Dfe.Testing.Pages.Components.Button;
using Dfe.Testing.Pages.Components.Fieldset;
using Dfe.Testing.Pages.Components.Form;
using Dfe.Testing.Pages.Internal.Components;
using HttpMethod = System.Net.Http.HttpMethod;

namespace Dfe.Testing.Pages.Internal.Components.Form;
internal sealed class FormMapper : IComponentMapper<FormComponent>
{
    private readonly ComponentFactory<GDSFieldsetComponent> _fieldSetFactory;
    private readonly ComponentFactory<GDSButtonComponent> _buttonFactory;

    public FormMapper(
        ComponentFactory<GDSFieldsetComponent> fieldSetFactory,
        ComponentFactory<GDSButtonComponent> buttonFactory)
    {
        ArgumentNullException.ThrowIfNull(fieldSetFactory);
        ArgumentNullException.ThrowIfNull(buttonFactory);
        _fieldSetFactory = fieldSetFactory;
        _buttonFactory = buttonFactory;
    }
    public FormComponent Map(IDocumentPart input)
    {
        return new FormComponent()
        {
            TagName = input.TagName,
            Method = HttpMethod.Parse(
                input.GetAttribute("method") ?? throw new ArgumentNullException(nameof(FormComponent.Method), "method on form is null")),
            FieldSets = _fieldSetFactory.GetMany(),
            Buttons = _buttonFactory.GetMany(),
            Action = input.GetAttribute("action") ?? throw new ArgumentNullException(nameof(FormComponent.Action), "action on form is null"),
            IsFormValidatedWithHTML = input.HasAttribute("novalidate")
        };
    }
}
