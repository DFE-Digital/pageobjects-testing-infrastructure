using Dfe.Testing.Pages.Components.Label;
using Dfe.Testing.Pages.Public.PageObjects;
using Dfe.Testing.Pages.Public.PageObjects.PageObjectClient.Request;
using Dfe.Testing.Pages.Public.PageObjects.PageObjectClient.Response;
using Dfe.Testing.Pages.Public.PageObjects.PageObjectClient.Templates;

namespace Dfe.Testing.Pages.Components.Input;
public record InputComponent
{
    public LabelComponent? Label { get; init; }
    public string? Id { get; init; }
    public string? Name { get; init; }
    public string? Value { get; init; }
    public string? Type { get; init; }
}

public sealed class InputComponentOptions
{
    public string Container { get; set; } = "InputContainer";
    public string Input { get; set; } = "InputContainer.Input";
    public string Label { get; set; } = "InputContainer.Label";
}

public sealed class InputMapper : IMapper<CreatedPageObjectModel, InputComponent>
{
    private readonly InputComponentOptions _inputOptions;

    public InputMapper(InputComponentOptions inputOptions)
    {
        _inputOptions = inputOptions;
    }
    public InputComponent Map(CreatedPageObjectModel mapFrom)
    {
        var input = mapFrom.Children.Single(t => t.Id == _inputOptions.Input);
        var label = mapFrom.Children.Single(t => t.Id == _inputOptions.Label);

        return new InputComponent()
        {

            Label = new()
            {
                For = label.GetAttribute("for"),
                Text = label.GetAttribute("text")
            },
            Id = input.GetAttribute("id"),
            Name = input.GetAttribute("name"),
            Type = input.GetAttribute("type"),
            Value = input.GetAttribute("value")
        };
    }
}

public sealed class InputComponentTemplate : IPageObjectTemplate
{
    private readonly InputComponentOptions _options;

    public InputComponentTemplate(
        InputComponentOptions options)
    {
        _options = options;
    }

    public string Id => nameof(InputComponent);
    public PageObjectSchema Schema => new()
    {
        Id = _options.Container,
        Find = "div:has(input)",
        Children = [
            new PageObjectSchema()
            {
                Id = _options.Input,
                Find = "input"
            },
            new PageObjectSchema()
            {
                Id = _options.Label,
                Find = "label"
            }
        ]
    };
}
