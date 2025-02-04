using Dfe.Testing.Pages.Public.Components.GDS.Checkbox;
using Dfe.Testing.Pages.Public.Components.GDS.Radio;
using Dfe.Testing.Pages.Public.Components.GDS.TextInput;
using Dfe.Testing.Pages.Public.Components.Text;

namespace Dfe.Testing.Pages.Public.Components.GDS.Fieldset;
internal sealed class GDSFieldsetMapper : IComponentMapper<GDSFieldsetComponent>
{
    private readonly IMapRequestFactory _mapRequestFactory;
    private readonly IMappingResultFactory _mappingResultFactory;
    private readonly IComponentMapper<GDSCheckboxComponent> _checkboxMapper;
    private readonly IComponentMapper<GDSRadioComponent> _radioMapper;
    private readonly IComponentMapper<GDSTextInputComponent> _textInputMapper;
    private readonly IComponentMapper<GDSSelectComponent> _selectMapper;
    private readonly IComponentMapper<TextComponent> _textMapper;

    public GDSFieldsetMapper(
        IMapRequestFactory mapRequestFactory,
        IMappingResultFactory mappingResultFactory,
        IComponentMapper<GDSCheckboxComponent> checkboxFactory,
        IComponentMapper<GDSRadioComponent> radioFactory,
        IComponentMapper<GDSTextInputComponent> textInput,
        IComponentMapper<GDSSelectComponent> selectComponentFactory,
        IComponentMapper<TextComponent> textMapper)
    {
        ArgumentNullException.ThrowIfNull(checkboxFactory);
        ArgumentNullException.ThrowIfNull(textInput);
        ArgumentNullException.ThrowIfNull(radioFactory);
        ArgumentNullException.ThrowIfNull(selectComponentFactory);
        ArgumentNullException.ThrowIfNull(textMapper);
        _mapRequestFactory = mapRequestFactory;
        _mappingResultFactory = mappingResultFactory;
        _checkboxMapper = checkboxFactory;
        _radioMapper = radioFactory;
        _textInputMapper = textInput;
        _selectMapper = selectComponentFactory;
        _textMapper = textMapper;
    }

    public MappedResponse<GDSFieldsetComponent> Map(IMapRequest<IDocumentSection> request)
    {
        MappedResponse<TextComponent> mappedLegend =
            _textMapper.Map(
                _mapRequestFactory.CreateRequestFrom(request, nameof(GDSFieldsetComponent.Legend)));

        IEnumerable<MappedResponse<GDSTextInputComponent>> mappedTextInputs =
            _mapRequestFactory.CreateRequestFrom(request, nameof(GDSFieldsetComponent.TextInputs))
                .FindManyDescendantsAndMapToComponent(_mapRequestFactory, _textInputMapper);

        IEnumerable<MappedResponse<GDSRadioComponent>> mappedRadios =
            _mapRequestFactory.CreateRequestFrom(request, nameof(GDSFieldsetComponent.Radios))
                .FindManyDescendantsAndMapToComponent(_mapRequestFactory, _radioMapper);

        IEnumerable<MappedResponse<GDSCheckboxComponent>> mappedCheckboxes =
            _mapRequestFactory.CreateRequestFrom(request, nameof(GDSFieldsetComponent.Checkboxes))
                .FindManyDescendantsAndMapToComponent(_mapRequestFactory, _checkboxMapper);


        GDSFieldsetComponent component = new()
        {
            Legend = mappedLegend.Mapped,
            TextInputs = mappedTextInputs.Select(t => t.Mapped),
            Radios = mappedRadios.Select(t => t.Mapped),
            Checkboxes = mappedCheckboxes.Select(t => t.Mapped)
        };

        return _mappingResultFactory.Create(
            request.Options.MapKey,
            component,
            MappingStatus.Success,
            request.Document)
                .AddToMappingResults(mappedLegend.MappingResults)
                .AddToMappingResults(mappedTextInputs.SelectMany(t => t.MappingResults))
                .AddToMappingResults(mappedRadios.SelectMany(t => t.MappingResults))
                .AddToMappingResults(mappedCheckboxes.SelectMany(t => t.MappingResults));
    }
}
