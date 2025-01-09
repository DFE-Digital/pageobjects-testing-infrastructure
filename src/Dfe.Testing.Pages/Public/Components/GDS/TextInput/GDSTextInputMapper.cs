using Dfe.Testing.Pages.Public.Components.GDS.ErrorMessage;
using Dfe.Testing.Pages.Public.Components.Inputs;
using Dfe.Testing.Pages.Public.Components.Label;
using Dfe.Testing.Pages.Public.Components.MappingAbstraction.Request;
using Dfe.Testing.Pages.Public.Components.SelectorFactory;

namespace Dfe.Testing.Pages.Public.Components.GDS.TextInput;
internal sealed class GDSTextInputMapper : IMapper<IMapRequest<IDocumentSection>, MappedResponse<GDSTextInputComponent>>
{
    private readonly IMapRequestFactory _mapRequestFactory;
    private readonly IComponentSelectorFactory _componentSelectorFactory;
    private readonly IMappingResultFactory _mappingResultFactory;
    private readonly IMapper<IMapRequest<IDocumentSection>, MappedResponse<TextInputComponent>> _textInputMapper;
    private readonly IMapper<IMapRequest<IDocumentSection>, MappedResponse<LabelComponent>> _labelMapper;
    private readonly IMapper<IMapRequest<IDocumentSection>, MappedResponse<GDSErrorMessageComponent>> _errorMessageMapper;

    public GDSTextInputMapper(
        IMapRequestFactory mapRequestFactory,
        IComponentSelectorFactory componentSelectorFactory,
        IMapper<IMapRequest<IDocumentSection>, MappedResponse<TextInputComponent>> textInputMapper,
        IMapper<IMapRequest<IDocumentSection>, MappedResponse<LabelComponent>> labelMapper,
        IMapper<IMapRequest<IDocumentSection>, MappedResponse<GDSErrorMessageComponent>> errorMessageMapper,
        IMappingResultFactory mappingResultFactory)
    {
        ArgumentNullException.ThrowIfNull(textInputMapper);
        ArgumentNullException.ThrowIfNull(labelMapper);
        ArgumentNullException.ThrowIfNull(errorMessageMapper);
        _mapRequestFactory = mapRequestFactory;
        _textInputMapper = textInputMapper;
        _labelMapper = labelMapper;
        _errorMessageMapper = errorMessageMapper;
        _mappingResultFactory = mappingResultFactory;
        _componentSelectorFactory = componentSelectorFactory;
    }

    public MappedResponse<GDSTextInputComponent> Map(IMapRequest<IDocumentSection> request)
    {
        MappedResponse<LabelComponent> label = _labelMapper.Map(
            _mapRequestFactory.Create(
                request.From,
                request.MappingResults,
                _componentSelectorFactory.GetSelector<LabelComponent>()));

        MappedResponse<TextInputComponent> text = _textInputMapper.Map(
            _mapRequestFactory.Create(
                request.From,
                request.MappingResults));

        MappedResponse<GDSErrorMessageComponent> errorMessage = _errorMessageMapper.Map(
            _mapRequestFactory.Create(
                request.From,
                request.MappingResults));

        GDSTextInputComponent component = new()
        {
            Label = label.Mapped,
            TextInput = text.Mapped,
            ErrorMessage = errorMessage.Mapped
        };

        return _mappingResultFactory.Create(
            component,
            MappingStatus.Success,
            request.From);
    }
}
