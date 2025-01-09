using Dfe.Testing.Pages.Public.Components.Checkbox;
using Dfe.Testing.Pages.Public.Components.GDS.ErrorMessage;
using Dfe.Testing.Pages.Public.Components.Label;
using Dfe.Testing.Pages.Public.Components.MappingAbstraction.Request;

namespace Dfe.Testing.Pages.Public.Components.GDS.Checkbox;
internal sealed class GDSCheckboxMapper : IMapper<IMapRequest<IDocumentSection>, MappedResponse<GDSCheckboxComponent>>
{
    private readonly IMappingResultFactory _mappingResultFactory;
    private readonly IGDSCheckboxBuilder _builder;
    private readonly IMapper<IMapRequest<IDocumentSection>, MappedResponse<CheckboxComponent>> _checkboxMapper;
    private readonly IMapper<IMapRequest<IDocumentSection>, MappedResponse<LabelComponent>> _labelMapper;
    private readonly IMapper<IMapRequest<IDocumentSection>, MappedResponse<GDSErrorMessageComponent>> _errorMessageMapper;

    public GDSCheckboxMapper(
        IMappingResultFactory mappingResultFactory,
        IGDSCheckboxBuilder builder,
        IMapper<IMapRequest<IDocumentSection>, MappedResponse<CheckboxComponent>> checkboxMapper,
        IMapper<IMapRequest<IDocumentSection>, MappedResponse<LabelComponent>> labelMapper,
        IMapper<IMapRequest<IDocumentSection>, MappedResponse<GDSErrorMessageComponent>> errorMessageMapper)
    {
        _mappingResultFactory = mappingResultFactory;
        _builder = builder;
        _checkboxMapper = checkboxMapper;
        _labelMapper = labelMapper;
        _errorMessageMapper = errorMessageMapper;
    }

    public MappedResponse<GDSCheckboxComponent> Map(IMapRequest<IDocumentSection> request)
    {
        MappedResponse<LabelComponent> mappedLabel = _labelMapper.Map(request).AddMappedResponseToResults(request.MappingResults);
        MappedResponse<CheckboxComponent> mappedCheckbox = _checkboxMapper.Map(request).AddMappedResponseToResults(request.MappingResults);
        MappedResponse<GDSErrorMessageComponent> mappedErrorMessage = _errorMessageMapper.Map(request).AddMappedResponseToResults(request.MappingResults);

        GDSCheckboxComponent component = _builder.SetCheckbox(mappedCheckbox.Mapped!)
            .SetLabelText(mappedLabel.Mapped!.Text!.Text)
            .SetLabelFor(mappedLabel.Mapped!.Text.Text)
            .SetErrorMessage(mappedErrorMessage.Mapped!.ErrorMessage.Text)
            .Build();

        return _mappingResultFactory.Create(
                component,
                MappingStatus.Success,
                request.From);
    }
}
