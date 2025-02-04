using Dfe.Testing.Pages.Public.Components.Checkbox;
using Dfe.Testing.Pages.Public.Components.GDS.ErrorMessage;
using Dfe.Testing.Pages.Public.Components.Label;

namespace Dfe.Testing.Pages.Public.Components.GDS.Checkbox;
internal sealed class GDSCheckboxMapper : IComponentMapper<GDSCheckboxComponent>
{
    private readonly IMapRequestFactory _mapRequestFactory;
    private readonly IMappingResultFactory _mappingResultFactory;
    private readonly IGDSCheckboxBuilder _builder;
    private readonly IComponentMapper<CheckboxComponent> _checkboxMapper;
    private readonly IComponentMapper<LabelComponent> _labelMapper;
    private readonly IComponentMapper<GDSErrorMessageComponent> _errorMessageMapper;

    public GDSCheckboxMapper(
        IMapRequestFactory mapRequestFactory,
        IMappingResultFactory mappingResultFactory,
        IGDSCheckboxBuilder builder,
        IComponentMapper<CheckboxComponent> checkboxMapper,
        IComponentMapper<LabelComponent> labelMapper,
        IComponentMapper<GDSErrorMessageComponent> errorMessageMapper)
    {
        _mapRequestFactory = mapRequestFactory;
        _mappingResultFactory = mappingResultFactory;
        _builder = builder;
        _checkboxMapper = checkboxMapper;
        _labelMapper = labelMapper;
        _errorMessageMapper = errorMessageMapper;
    }

    public MappedResponse<GDSCheckboxComponent> Map(IMapRequest<IDocumentSection> request)
    {
        MappedResponse<LabelComponent> mappedLabel =
            _labelMapper.Map(
                _mapRequestFactory.CreateRequestFrom(request, nameof(GDSCheckboxComponent.Label)))
            .AddToMappingResults(request.MappedResults);

        MappedResponse<CheckboxComponent> mappedCheckbox =
            _checkboxMapper.Map(
                _mapRequestFactory.CreateRequestFrom(request, nameof(GDSCheckboxComponent.Checkbox)))
            .AddToMappingResults(request.MappedResults);

        MappedResponse<GDSErrorMessageComponent> mappedErrorMessage =
            _errorMessageMapper.Map(
                _mapRequestFactory.CreateRequestFrom(request, nameof(GDSCheckboxComponent.Error)))
            .AddToMappingResults(request.MappedResults);

        GDSCheckboxComponent component =
            _builder.SetCheckbox(mappedCheckbox.Mapped!)
                .SetLabelText(mappedLabel.Mapped!.Text!.Text)
                .SetLabelFor(mappedLabel.Mapped!.Text.Text)
                .SetErrorMessage(mappedErrorMessage.Mapped!.ErrorMessage.Text)
                .Build();

        return _mappingResultFactory.Create(
                request.Options.MapKey,
                component,
                MappingStatus.Success,
                request.Document);
    }
}
