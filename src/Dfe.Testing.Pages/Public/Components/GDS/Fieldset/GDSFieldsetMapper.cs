using Dfe.Testing.Pages.Public.Components.GDS.Checkbox;
using Dfe.Testing.Pages.Public.Components.GDS.Radio;
using Dfe.Testing.Pages.Public.Components.GDS.TextInput;
using Dfe.Testing.Pages.Public.Components.MappingAbstraction.Request;
using Dfe.Testing.Pages.Public.Components.SelectorFactory;
using Dfe.Testing.Pages.Public.Components.Text;

namespace Dfe.Testing.Pages.Public.Components.GDS.Fieldset;
internal sealed class GDSFieldsetMapper : IMapper<IMapRequest<IDocumentSection>, MappedResponse<GDSFieldsetComponent>>
{
    private readonly IMapRequestFactory _mapRequestFactory;
    private readonly IMappingResultFactory _mappingResultFactory;
    private readonly IComponentSelectorFactory _componentSelectorFactory;
    private readonly IMapper<IMapRequest<IDocumentSection>, MappedResponse<GDSCheckboxComponent>> _checkboxMapper;
    private readonly IMapper<IMapRequest<IDocumentSection>, MappedResponse<GDSRadioComponent>> _radioMapper;
    private readonly IMapper<IMapRequest<IDocumentSection>, MappedResponse<GDSTextInputComponent>> _textInputMapper;
    private readonly IMapper<IMapRequest<IDocumentSection>, MappedResponse<GDSSelectComponent>> _selectMapper;
    private readonly IMapper<IMapRequest<IDocumentSection>, MappedResponse<TextComponent>> _textMapper;

    public GDSFieldsetMapper(
        IMapRequestFactory mapRequestFactory,
        IMappingResultFactory mappingResultFactory,
        IComponentSelectorFactory componentSelectorFactory,
        IMapper<IMapRequest<IDocumentSection>, MappedResponse<GDSCheckboxComponent>> checkboxFactory,
        IMapper<IMapRequest<IDocumentSection>, MappedResponse<GDSRadioComponent>> radioFactory,
        IMapper<IMapRequest<IDocumentSection>, MappedResponse<GDSTextInputComponent>> textInput,
        IMapper<IMapRequest<IDocumentSection>, MappedResponse<GDSSelectComponent>> selectComponentFactory,
        IMapper<IMapRequest<IDocumentSection>, MappedResponse<TextComponent>> textMapper)
    {
        ArgumentNullException.ThrowIfNull(checkboxFactory);
        ArgumentNullException.ThrowIfNull(textInput);
        ArgumentNullException.ThrowIfNull(radioFactory);
        ArgumentNullException.ThrowIfNull(selectComponentFactory);
        ArgumentNullException.ThrowIfNull(textMapper);
        _mapRequestFactory = mapRequestFactory;
        _mappingResultFactory = mappingResultFactory;
        _componentSelectorFactory = componentSelectorFactory;
        _checkboxMapper = checkboxFactory;
        _radioMapper = radioFactory;
        _textInputMapper = textInput;
        _selectMapper = selectComponentFactory;
        _textMapper = textMapper;
    }
    public MappedResponse<GDSFieldsetComponent> Map(IMapRequest<IDocumentSection> request)
    {
        //Legend
        MappedResponse<TextComponent> mappedLegend = _textMapper.Map(
            _mapRequestFactory.Create(
                request.From,
                request.MappingResults,
                new CssElementSelector("legend")));
        request.MappingResults.Add(mappedLegend.MappingResult);

        // TextInputs
        IEnumerable<MappedResponse<GDSTextInputComponent>> mappedTextInputs = request.FindManyDescendantsAndMap(
            _mapRequestFactory,
            _componentSelectorFactory.GetSelector<GDSTextInputComponent>(),
            _textInputMapper)
        .AddMappedResponseToResults(request.MappingResults);

        // Radios
        IEnumerable<MappedResponse<GDSRadioComponent>> mappedRadios = request.FindManyDescendantsAndMap(
            _mapRequestFactory,
            _componentSelectorFactory.GetSelector<GDSRadioComponent>(),
            _radioMapper)
        .AddMappedResponseToResults(request.MappingResults);

        // Checkboxes
        IEnumerable<MappedResponse<GDSCheckboxComponent>> mappedCheckboxes = request.FindManyDescendantsAndMap(
            _mapRequestFactory,
            _componentSelectorFactory.GetSelector<GDSCheckboxComponent>(),
            _checkboxMapper)
        .AddMappedResponseToResults(request.MappingResults);


        GDSFieldsetComponent component = new()
        {
            Legend = mappedLegend.Mapped,
            TextInputs = mappedTextInputs.Select(t => t.Mapped),
            Radios = mappedRadios.Select(t => t.Mapped),
            Checkboxes = mappedCheckboxes.Select(t => t.Mapped)
        };

        return _mappingResultFactory.Create(
            component,
            MappingStatus.Success,
            request.From);
    }
}
