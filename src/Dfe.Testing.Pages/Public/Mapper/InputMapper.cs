using Dfe.Testing.Pages.Components.ErrorMessage;
using Dfe.Testing.Pages.Components.Inputs;
using Dfe.Testing.Pages.Public.Mapper.Abstraction;
using Dfe.Testing.Pages.Public.Selector.Factory;

namespace Dfe.Testing.Pages.Public.Mapper;
internal class InputMapper : IComponentMapper<InputComponent>
{
    private readonly ComponentFactory<GDSErrorMessageComponent> _errorMessageFactory;
    private readonly IComponentSelectorFactory _componentDefaultSelectorFactory;

    public InputMapper(ComponentFactory<GDSErrorMessageComponent> errorMessageFactory, IComponentSelectorFactory componentDefaultSelectorFactory)
    {
        ArgumentNullException.ThrowIfNull(errorMessageFactory);
        ArgumentNullException.ThrowIfNull(componentDefaultSelectorFactory);
        _errorMessageFactory = errorMessageFactory;
        _componentDefaultSelectorFactory = componentDefaultSelectorFactory;
    }
    public InputComponent Map(IDocumentPart from)
    {
        var mappable = from.TagName == "input" ?
            from :
            from.FindDescendant(_componentDefaultSelectorFactory.GetSelector<InputComponent>()) ?? throw new ArgumentNullException("could not find child InputComponent");

        var errors = _errorMessageFactory.GetManyFromPart(from) ?? [NoErrorMessage()];

        return new()
        {
            Name = mappable.GetAttribute("name") ?? string.Empty,
            Type = mappable.GetAttribute("type") ?? string.Empty,
            Value = mappable.GetAttribute("value") ?? string.Empty,
            IsNumeric = mappable.GetAttribute("inputmode") == "numeric",
            ErrorMessage = errors.Count > 0
                            ? errors.First()
                            : NoErrorMessage(),
            AutoComplete = mappable.GetAttribute("autocomplete") ?? string.Empty,
            PlaceHolder = mappable.GetAttribute("placeholder") ?? string.Empty,
        };
    }

    private static GDSErrorMessageComponent NoErrorMessage() => new() { ErrorMessage = string.Empty };
}
