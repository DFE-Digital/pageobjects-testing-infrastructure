using Dfe.Testing.Pages.Public.Components.GDS.ErrorMessage;
using Dfe.Testing.Pages.Public.Components.Label;
using Dfe.Testing.Pages.Public.Components.TextInput;

namespace Dfe.Testing.Pages.Public.Components.GDS.TextInput;
internal sealed class GDSTextInputMapper : IComponentMapper<GDSTextInputComponent>
{
    private readonly IMapRequestFactory _mapRequestFactory;
    private readonly IMappingResultFactory _mappingResultFactory;
    private readonly IComponentMapper<TextInputComponent> _textInputMapper;
    private readonly IComponentMapper<LabelComponent> _labelMapper;
    private readonly IComponentMapper<GDSErrorMessageComponent> _errorMessageMapper;

    public GDSTextInputMapper(
        IMapRequestFactory mapRequestFactory,
        IComponentMapper<TextInputComponent> textInputMapper,
        IComponentMapper<LabelComponent> labelMapper,
        IComponentMapper<GDSErrorMessageComponent> errorMessageMapper,
        IMappingResultFactory mappingResultFactory)
    {
        ArgumentNullException.ThrowIfNull(textInputMapper);
        ArgumentNullException.ThrowIfNull(labelMapper);
        ArgumentNullException.ThrowIfNull(errorMessageMapper);
        _textInputMapper = textInputMapper;
        _labelMapper = labelMapper;
        _errorMessageMapper = errorMessageMapper;
        _mappingResultFactory = mappingResultFactory;
        _mapRequestFactory = mapRequestFactory;
    }

    public MappedResponse<GDSTextInputComponent> Map(IMapRequest<IDocumentSection> request)
    {
        MappedResponse<LabelComponent> label = _labelMapper.Map(
            _mapRequestFactory.CreateRequestFrom(request, nameof(GDSTextInputComponent.Label)))
                .AddToMappingResults(request.MappedResults);

        MappedResponse<TextInputComponent> text = _textInputMapper.Map(
            _mapRequestFactory.CreateRequestFrom(request, nameof(GDSTextInputComponent.TextInput)))
                .AddToMappingResults(request.MappedResults);

        MappedResponse<GDSErrorMessageComponent> errorMessage = _errorMessageMapper.Map(
            _mapRequestFactory.CreateRequestFrom(request, nameof(GDSTextInputComponent.ErrorMessage)))
                .AddToMappingResults(request.MappedResults);

        GDSTextInputComponent component = new()
        {
            Label = label.Mapped,
            TextInput = text.Mapped,
            ErrorMessage = errorMessage.Mapped
        };

        return _mappingResultFactory.Create(
            component,
            MappingStatus.Success,
            request.Document);
    }
}
