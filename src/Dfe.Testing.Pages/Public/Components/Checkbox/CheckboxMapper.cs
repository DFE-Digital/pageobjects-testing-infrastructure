using Dfe.Testing.Pages.Public.Components.MappingAbstraction.Request;

namespace Dfe.Testing.Pages.Public.Components.Checkbox;
internal sealed class CheckboxMapper : IMapper<IMapRequest<IDocumentSection>, MappedResponse<CheckboxComponent>>
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
            .SetChecked(request.From.HasAttribute("checked"))
            .SetId(request.From.GetAttribute("id") ?? string.Empty)
            .SetRequired(request.From.HasAttribute("required"))
            .SetName(request.From.GetAttribute("name") ?? string.Empty)
            .SetValue(request.From.GetAttribute("value") ?? string.Empty)
            .Build();

        return _mappingResultFactory.Create(
                component,
                MappingStatus.Success,
                request.From);
    }
}
