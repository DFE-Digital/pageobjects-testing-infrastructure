namespace Dfe.Testing.Pages.Public.Components.Checkbox;
internal sealed class CheckboxMapper : IComponentMapper<CheckboxComponent>
{
    private readonly IMappingResultFactory _mappingResultFactory;
    private readonly ICheckboxBuilder _checkboxBuilder;

    public CheckboxMapper(
        IMappingResultFactory mappingResultFactory,
        ICheckboxBuilder checkboxBuilder)
    {
        _mappingResultFactory = mappingResultFactory;
        _checkboxBuilder = checkboxBuilder;
    }

    public MappedResponse<CheckboxComponent> Map(IMapRequest<IDocumentSection> request)
    {
        CheckboxComponent component = _checkboxBuilder
            .SetChecked(request.Document.HasAttribute("checked"))
            .SetId(request.Document.GetAttribute("id") ?? string.Empty)
            .SetRequired(request.Document.HasAttribute("required"))
            .SetName(request.Document.GetAttribute("name") ?? string.Empty)
            .SetValue(request.Document.GetAttribute("value") ?? string.Empty)
            .Build();

        return _mappingResultFactory.Create(
                component,
                MappingStatus.Success,
                request.Document);
    }
}
