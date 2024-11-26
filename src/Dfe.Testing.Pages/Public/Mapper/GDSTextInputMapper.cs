using Dfe.Testing.Pages.Components.TextInput;
using Dfe.Testing.Pages.Public.Mapper.Abstraction;

namespace Dfe.Testing.Pages.Public.Mapper;
internal class GDSTextInputMapper : IComponentMapper<GDSTextInputComponent>
{
    public GDSTextInputComponent Map(IDocumentPart input)
    {
        var textInput = input.GetChild(new CssSelector("input[type=text]"))!;
        return new GDSTextInputComponent()
        {
            Label = input.GetChild(new CssSelector("label"))!.Text ?? string.Empty,
            Hint = input.GetChild(new CssSelector(".govuk-hint"))?.Text ?? string.Empty,
            ErrorMessage = input.GetChild(new CssSelector(".govuk-error-message"))?.Text ?? string.Empty,
            Name = textInput.GetAttribute("name") ?? string.Empty,
            TagName = input.TagName ?? string.Empty,
            AutoComplete = textInput.GetAttribute("autocomplete") ?? string.Empty,
            PlaceHolder = textInput.GetAttribute("placeholder") ?? string.Empty,
            IsNumeric = textInput.GetAttribute("inputmode") == "numeric",
            Type = textInput.GetAttribute("type") ?? string.Empty,
        };
    }
}
