using Dfe.Testing.Pages.Public.Components.GDS.ErrorMessage;
using Dfe.Testing.Pages.Public.Components.Label;
using Dfe.Testing.Pages.Public.Components.Radio;

namespace Dfe.Testing.Pages.Public.Components.GDS.Radio;
internal sealed class GDSRadioMapper : IComponentMapper<GDSRadioComponent>
{
    private readonly IMapRequestFactory _mapRequestFactory;
    private readonly IMappingResultFactory _mappingResultFactory;
    private readonly IComponentMapper<LabelComponent> _labelMapper;
    private readonly IComponentMapper<RadioComponent> _radioMapper;
    private readonly IComponentMapper<GDSErrorMessageComponent> _errorMessageMapper;

    public GDSRadioMapper(
        IMapRequestFactory mapRequestFactory,
        IMappingResultFactory mappingResultFactory,
        IComponentMapper<LabelComponent> labelMapper,
        IComponentMapper<RadioComponent> radioMapper,
        IComponentMapper<GDSErrorMessageComponent> errorMessageMapper)
    {
        _mappingResultFactory = mappingResultFactory;
        _labelMapper = labelMapper;
        _radioMapper = radioMapper;
        _errorMessageMapper = errorMessageMapper;
        _mapRequestFactory = mapRequestFactory;
    }

    public MappedResponse<GDSRadioComponent> Map(IMapRequest<IDocumentSection> request)
    {
        MappedResponse<LabelComponent> mappedLabel =
            _labelMapper.Map(
                _mapRequestFactory.CreateRequestFrom(request, nameof(GDSRadioComponent.Label)))
            .AddToMappingResults(request.MappedResults);

        MappedResponse<RadioComponent> mappedRadio =
            _radioMapper.Map(
                _mapRequestFactory.CreateRequestFrom(request, nameof(GDSRadioComponent.Radio)))
            .AddToMappingResults(request.MappedResults);

        return _mappingResultFactory.Create(
            request.Options.MapKey,
            new GDSRadioComponent()
            {
                Label = mappedLabel.Mapped,
                Radio = mappedRadio.Mapped,
                Error = _errorMessageMapper.Map(request).Mapped
            },
            MappingStatus.Success,
            request.Document);
    }
}
