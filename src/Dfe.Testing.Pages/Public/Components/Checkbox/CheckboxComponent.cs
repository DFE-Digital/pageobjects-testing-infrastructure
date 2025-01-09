namespace Dfe.Testing.Pages.Public.Components.Checkbox;
public record CheckboxComponent
{
    internal CheckboxComponent() { }
    public required string Name { get; init; }
    public required string Value { get; init; }
    public string Id { get; init; } = string.Empty;
    public bool IsChecked { get; init; } = false;
    public bool IsRequired { get; init; } = false;
}

public interface ICheckboxBuilder
{
    public ICheckboxBuilder SetValue(string value);
    public ICheckboxBuilder SetChecked(bool isChecked);
    public ICheckboxBuilder SetName(string name);
    public ICheckboxBuilder SetId(string id);
    public ICheckboxBuilder SetRequired(bool isRequired);
    public CheckboxComponent Build();
}
