using Dfe.Testing.Pages.Components.ErrorMessage;
using Dfe.Testing.Pages.Components.Input;
using Dfe.Testing.Pages.Public.Mapper.Abstraction;

namespace Dfe.Testing.Pages.Public.Mapper;
internal class InputMapper : IComponentMapper<InputComponent>
{
    private readonly ComponentFactory<GDSErrorMessageComponent> _errorMessageFactory;

    public InputMapper(ComponentFactory<GDSErrorMessageComponent> errorMessageFactory)
    {
        ArgumentNullException.ThrowIfNull(errorMessageFactory);
        _errorMessageFactory = errorMessageFactory;
    }
    public InputComponent Map(IDocumentPart input)
    {
        return new()
        {
            Name = input.GetAttribute("name") ?? string.Empty,
            Type = input.GetAttribute("type") ?? string.Empty,
            Value = input.GetAttribute("value") ?? string.Empty,
            IsNumeric = input.GetAttribute("inputmode") == "numeric",
            ErrorMessage = _errorMessageFactory.GetManyFromPart(input).Single(),
            AutoComplete = input.GetAttribute("autocomplete") ?? string.Empty,
            PlaceHolder = input.GetAttribute("placeholder") ?? string.Empty,
        };
    }
}
