using Dfe.Testing.Pages.Components.ErrorMessage;
using Dfe.Testing.Pages.Components.TextInput;
using Dfe.Testing.Pages.Public.Mapper.Abstraction;

namespace Dfe.Testing.Pages.Public.Mapper;
internal class GDSTextInputMapper : IComponentMapper<GDSTextInputComponent>
{
    private readonly ComponentFactory<GDSErrorMessageComponent> _errorMessageFactory;

    public GDSTextInputMapper(ComponentFactory<GDSErrorMessageComponent> errorMessageFactory)
    {
        ArgumentNullException.ThrowIfNull(errorMessageFactory);
        this._errorMessageFactory = errorMessageFactory;
    }
    public GDSTextInputComponent Map(IDocumentPart input)
    {
        var textInput = input.GetChild(new CssSelector("input[type=text]"))!;
        return new GDSTextInputComponent()
        {
            Label = input.GetChild(new CssSelector("label"))!.Text ?? string.Empty,
            Hint = input.GetChild(new CssSelector(".govuk-hint"))?.Text ?? string.Empty,
            ErrorMessage = _errorMessageFactory.GetManyFromPart(input).Single(),
            Name = textInput.GetAttribute("name") ?? string.Empty,
            TagName = input.TagName ?? string.Empty,
            AutoComplete = textInput.GetAttribute("autocomplete") ?? string.Empty,
            PlaceHolder = textInput.GetAttribute("placeholder") ?? string.Empty,
            IsNumeric = textInput.GetAttribute("inputmode") == "numeric",
            Type = textInput.GetAttribute("type") ?? string.Empty,
        };
    }
}
