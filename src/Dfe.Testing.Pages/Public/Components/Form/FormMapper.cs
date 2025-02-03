using Dfe.Testing.Pages.Public.Components.GDS.Button;
using Dfe.Testing.Pages.Public.Components.GDS.Checkbox;
using Dfe.Testing.Pages.Public.Components.GDS.Fieldset;
using Dfe.Testing.Pages.Public.Components.GDS.Radio;
using Dfe.Testing.Pages.Public.Components.GDS.TextInput;
using Dfe.Testing.Pages.Public.Components.HiddenInput;
using HttpMethod = System.Net.Http.HttpMethod;

namespace Dfe.Testing.Pages.Public.Components.Form;
internal sealed class FormMapper : IComponentMapper<FormComponent>
{
    private readonly IMapRequestFactory _mapRequestFactory;
    private readonly IMappingResultFactory _mappingResultFactory;
    private readonly IComponentMapper<GDSFieldsetComponent> _GdsfieldsetMapper;
    private readonly IComponentMapper<GDSButtonComponent> _GdsButtonMapper;
    private readonly IComponentMapper<GDSTextInputComponent> _GdsTextInputMapper;
    private readonly IComponentMapper<GDSCheckboxComponent> _GdsCheckboxMapper;
    private readonly IComponentMapper<GDSRadioComponent> _GdsRadioMapper;
    private readonly IComponentMapper<GDSSelectComponent> _GdsSelectMapper;
    private readonly IComponentMapper<HiddenInputComponent> _hiddenInputMapper;

    public FormMapper(
        IMappingResultFactory mappingResultFactory,
        IComponentMapper<GDSFieldsetComponent> GdsfieldsetMapper,
        IComponentMapper<GDSButtonComponent> GdsbuttonMapper,
        IComponentMapper<GDSTextInputComponent> GdsTextInputMapper,
        IComponentMapper<GDSCheckboxComponent> GdsCheckboxMapper,
        IComponentMapper<GDSRadioComponent> GdsRadioMapper,
        IComponentMapper<GDSSelectComponent> GdsSelectMapper,
        IComponentMapper<HiddenInputComponent> hiddenInputFactory,
        IMapRequestFactory mapRequestFactory)
    {
        ArgumentNullException.ThrowIfNull(GdsfieldsetMapper);
        ArgumentNullException.ThrowIfNull(GdsbuttonMapper);
        ArgumentNullException.ThrowIfNull(GdsTextInputMapper);
        ArgumentNullException.ThrowIfNull(GdsCheckboxMapper);
        ArgumentNullException.ThrowIfNull(GdsRadioMapper);
        ArgumentNullException.ThrowIfNull(GdsSelectMapper);
        ArgumentNullException.ThrowIfNull(hiddenInputFactory);
        _GdsfieldsetMapper = GdsfieldsetMapper;
        _GdsButtonMapper = GdsbuttonMapper;
        _GdsTextInputMapper = GdsTextInputMapper;
        _GdsCheckboxMapper = GdsCheckboxMapper;
        _GdsRadioMapper = GdsRadioMapper;
        _GdsSelectMapper = GdsSelectMapper;
        _hiddenInputMapper = hiddenInputFactory;
        _mappingResultFactory = mappingResultFactory;
        _mapRequestFactory = mapRequestFactory;
    }

    public MappedResponse<FormComponent> Map(IMapRequest<IDocumentSection> request)
    {
        IEnumerable<MappedResponse<GDSFieldsetComponent>> fieldsets =
            _mapRequestFactory.CreateRequestFrom(request, nameof(FormComponent.Fieldsets))
                .FindManyDescendantsAndMapToComponent(_mapRequestFactory, _GdsfieldsetMapper)
                .AddToMappingResults(request.MappedResults);

        IEnumerable<MappedResponse<GDSButtonComponent>> mappedButtons =
            _mapRequestFactory.CreateRequestFrom(request, nameof(FormComponent.Buttons))
                .FindManyDescendantsAndMapToComponent(_mapRequestFactory, _GdsButtonMapper)
                .AddToMappingResults(request.MappedResults);

        IEnumerable<MappedResponse<HiddenInputComponent>> mappedHiddenInputs =
            _mapRequestFactory.CreateRequestFrom(request, nameof(FormComponent.HiddenInputs))
                .FindManyDescendantsAndMapToComponent(_mapRequestFactory, _hiddenInputMapper)
                .AddToMappingResults(request.MappedResults);

        IEnumerable<MappedResponse<GDSTextInputComponent>> mappedTextInputs =
            _mapRequestFactory.CreateRequestFrom(request, nameof(FormComponent.TextInputs))
                .FindManyDescendantsAndMapToComponent(_mapRequestFactory, _GdsTextInputMapper)
                .AddToMappingResults(request.MappedResults);

        IEnumerable<MappedResponse<GDSCheckboxComponent>> mappedCheckboxes =
            _mapRequestFactory.CreateRequestFrom(request, nameof(FormComponent.Checkboxes))
                .FindManyDescendantsAndMapToComponent(_mapRequestFactory, _GdsCheckboxMapper)
                .AddToMappingResults(request.MappedResults);

        IEnumerable<MappedResponse<GDSRadioComponent>> mappedRadios =
            _mapRequestFactory.CreateRequestFrom(request, nameof(FormComponent.Radios))
                .FindManyDescendantsAndMapToComponent(_mapRequestFactory, _GdsRadioMapper)
                .AddToMappingResults(request.MappedResults);

        IEnumerable<MappedResponse<GDSSelectComponent>> mappedSelects =
            _mapRequestFactory.CreateRequestFrom(request, nameof(FormComponent.Selects))
                .FindManyDescendantsAndMapToComponent(_mapRequestFactory, _GdsSelectMapper)
                .AddToMappingResults(request.MappedResults);

        FormComponent form = new()
        {
            // form attributes
            // Default method is GET if not specified
            Method = HttpMethod.Parse(request.Document.GetAttribute("method") ?? "get"),
            Action = request.Document.GetAttribute("action") ?? string.Empty,
            IsFormValidated = !request.Document.HasAttribute("novalidate"),
            // form parts
            Fieldsets = fieldsets.Select(t => t.Mapped),
            Buttons = mappedButtons.Select(t => t.Mapped),
            HiddenInputs = mappedHiddenInputs.Select(t => t.Mapped),
            TextInputs = mappedTextInputs.Select(t => t.Mapped),
            Checkboxes = mappedCheckboxes.Select(t => t.Mapped),
            Radios = mappedRadios.Select(t => t.Mapped),
            Selects = mappedSelects.Select(t => t.Mapped)
        };

        return _mappingResultFactory.Create(
            mapped: form,
            status: MappingStatus.Success,
            request.Document);
    }
}
