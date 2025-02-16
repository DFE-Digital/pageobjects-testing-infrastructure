using Dfe.Testing.Pages.Public.Components.GDS.Button;
using Dfe.Testing.Pages.Public.Components.GDS.Checkbox;
using Dfe.Testing.Pages.Public.Components.GDS.Fieldset;
using Dfe.Testing.Pages.Public.Components.GDS.Radio;
using Dfe.Testing.Pages.Public.Components.GDS.TextInput;
using Dfe.Testing.Pages.Public.Components.HiddenInput;
using Dfe.Testing.Pages.Public.Components.MappingAbstraction.Response;
using HttpMethod = System.Net.Http.HttpMethod;

namespace Dfe.Testing.Pages.Public.Components.Form;
internal sealed class FormMapper : IComponentMapper<FormComponentOld>
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

    public MappedResponse<FormComponentOld> Map(IMapRequest<IDocumentSection> request)
    {
        IEnumerable<MappedResponse<GDSFieldsetComponent>> fieldsets =
            _mapRequestFactory.CreateRequestFrom(request, nameof(FormComponentOld.Fieldsets))
                .FindManyDescendantsAndMapToComponent(_mapRequestFactory, _GdsfieldsetMapper);

        IEnumerable<MappedResponse<GDSButtonComponent>> mappedButtons =
            _mapRequestFactory.CreateRequestFrom(request, nameof(FormComponentOld.Buttons))
                .FindManyDescendantsAndMapToComponent(_mapRequestFactory, _GdsButtonMapper);

        IEnumerable<MappedResponse<HiddenInputComponent>> mappedHiddenInputs =
            _mapRequestFactory.CreateRequestFrom(request, nameof(FormComponentOld.HiddenInputs))
                .FindManyDescendantsAndMapToComponent(_mapRequestFactory, _hiddenInputMapper);

        IEnumerable<MappedResponse<GDSTextInputComponent>> mappedTextInputs =
            _mapRequestFactory.CreateRequestFrom(request, nameof(FormComponentOld.TextInputs))
                .FindManyDescendantsAndMapToComponent(_mapRequestFactory, _GdsTextInputMapper);

        IEnumerable<MappedResponse<GDSCheckboxComponent>> mappedCheckboxes =
            _mapRequestFactory.CreateRequestFrom(request, nameof(FormComponentOld.Checkboxes))
                .FindManyDescendantsAndMapToComponent(_mapRequestFactory, _GdsCheckboxMapper);

        IEnumerable<MappedResponse<GDSRadioComponent>> mappedRadios =
            _mapRequestFactory.CreateRequestFrom(request, nameof(FormComponentOld.Radios))
                .FindManyDescendantsAndMapToComponent(_mapRequestFactory, _GdsRadioMapper);

        IEnumerable<MappedResponse<GDSSelectComponent>> mappedSelects =
            _mapRequestFactory.CreateRequestFrom(request, nameof(FormComponentOld.Selects))
                .FindManyDescendantsAndMapToComponent(_mapRequestFactory, _GdsSelectMapper);

        FormComponentOld form = new()
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

        MappedResponse<FormComponentOld> mappedResponse = _mappingResultFactory.Create(
            request.Options.MapKey,
            mapped: form,
            status: MappingStatus.Success,
            request.Document)
        .AddToMappingResults(fieldsets.SelectMany(t => t.MappingResults))
        .AddToMappingResults(mappedButtons.SelectMany(t => t.MappingResults))
        .AddToMappingResults(mappedHiddenInputs.SelectMany(t => t.MappingResults))
        .AddToMappingResults(mappedTextInputs.SelectMany(t => t.MappingResults))
        .AddToMappingResults(mappedCheckboxes.SelectMany(t => t.MappingResults))
        .AddToMappingResults(mappedRadios.SelectMany(t => t.MappingResults))
        .AddToMappingResults(mappedSelects.SelectMany(t => t.MappingResults));

        return mappedResponse;
    }
}
