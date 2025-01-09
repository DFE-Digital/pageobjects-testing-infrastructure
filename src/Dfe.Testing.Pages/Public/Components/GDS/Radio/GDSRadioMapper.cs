using Dfe.Testing.Pages.Public.Components.GDS.ErrorMessage;
using Dfe.Testing.Pages.Public.Components.Label;
using Dfe.Testing.Pages.Public.Components.MappingAbstraction.Request;
using Dfe.Testing.Pages.Public.Components.Radio;
using Dfe.Testing.Pages.Public.Components.SelectorFactory;

namespace Dfe.Testing.Pages.Public.Components.GDS.Radio;
internal sealed class GDSRadioMapper : IMapper<IMapRequest<IDocumentSection>, MappedResponse<GDSRadioComponent>>
{
    private readonly IMapRequestFactory _mapRequestFactory;
    private readonly IMappingResultFactory _mappingResultFactory;
    private readonly IComponentSelectorFactory _componentSelectorFactory;
    private readonly IMapper<IMapRequest<IDocumentSection>, MappedResponse<LabelComponent>> _labelMapper;
    private readonly IMapper<IMapRequest<IDocumentSection>, MappedResponse<RadioComponent>> _radioMapper;
    private readonly IMapper<IMapRequest<IDocumentSection>, MappedResponse<GDSErrorMessageComponent>> _errorMessageMapper;

    public GDSRadioMapper(
        IMapRequestFactory mapRequestFactory,
        IMappingResultFactory mappingResultFactory,
        IComponentSelectorFactory componentSelectorFactory,
        IMapper<IMapRequest<IDocumentSection>, MappedResponse<LabelComponent>> labelMapper,
        IMapper<IMapRequest<IDocumentSection>, MappedResponse<RadioComponent>> radioMapper,
        IMapper<IMapRequest<IDocumentSection>, MappedResponse<GDSErrorMessageComponent>> errorMessageMapper)
    {
        _mappingResultFactory = mappingResultFactory;
        _labelMapper = labelMapper;
        _radioMapper = radioMapper;
        _errorMessageMapper = errorMessageMapper;
        _componentSelectorFactory = componentSelectorFactory;
        _mapRequestFactory = mapRequestFactory;
    }
    public MappedResponse<GDSRadioComponent> Map(IMapRequest<IDocumentSection> request)
    {
        MappedResponse<LabelComponent> mappedLabel = _labelMapper.Map(
            _mapRequestFactory.Create(
                request.From,
                request.MappingResults,
                request.EntryPoint ?? _componentSelectorFactory.GetSelector<LabelComponent>()))
            .AddMappedResponseToResults(request.MappingResults);

        MappedResponse<RadioComponent> mappedRadio = _radioMapper.Map(
            _mapRequestFactory.Create(
                request.From,
                request.MappingResults,
                request.EntryPoint ?? _componentSelectorFactory.GetSelector<RadioComponent>()))
            .AddMappedResponseToResults(request.MappingResults);

        GDSRadioComponent radio = new()
        {
            Label = mappedLabel.Mapped,
            Radio = mappedRadio.Mapped,
            Error = _errorMessageMapper.Map(request).Mapped
        };

        return _mappingResultFactory.Create(
            radio,
            MappingStatus.Success,
            request.From);
    }
}
