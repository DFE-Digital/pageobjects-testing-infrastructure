using Dfe.Testing.Pages.Public.Components.GDS.Button;
using Dfe.Testing.Pages.Public.Components.GDS.Checkbox;
using Dfe.Testing.Pages.Public.Components.GDS.Fieldset;
using Dfe.Testing.Pages.Public.Components.GDS.Radio;
using Dfe.Testing.Pages.Public.Components.GDS.TextInput;
using Dfe.Testing.Pages.Public.Components.HiddenInput;
using Dfe.Testing.Pages.Public.Components.MappingAbstraction.Request;
using Dfe.Testing.Pages.Public.Components.SelectorFactory;
using HttpMethod = System.Net.Http.HttpMethod;

namespace Dfe.Testing.Pages.Public.Components.Form;
internal sealed class FormMapper : IMapper<IMapRequest<IDocumentSection>, MappedResponse<FormComponent>>
{
    private readonly IMapRequestFactory _mapRequestFactory;
    private readonly IComponentSelectorFactory _componentSelectorFactory;
    private readonly IMappingResultFactory _mappingResultFactory;
    private readonly IMapper<IMapRequest<IDocumentSection>, MappedResponse<GDSFieldsetComponent>> _GdsfieldsetMapper;
    private readonly IMapper<IMapRequest<IDocumentSection>, MappedResponse<GDSButtonComponent>> _GdsButtonMapper;
    private readonly IMapper<IMapRequest<IDocumentSection>, MappedResponse<GDSTextInputComponent>> _GdsTextInputMapper;
    private readonly IMapper<IMapRequest<IDocumentSection>, MappedResponse<GDSCheckboxComponent>> _GdsCheckboxMapper;
    private readonly IMapper<IMapRequest<IDocumentSection>, MappedResponse<GDSRadioComponent>> _GdsRadioMapper;
    private readonly IMapper<IMapRequest<IDocumentSection>, MappedResponse<GDSSelectComponent>> _GdsSelectMapper;
    private readonly IMapper<IMapRequest<IDocumentSection>, MappedResponse<HiddenInputComponent>> _hiddenInputMapper;

    public FormMapper(
        IMapRequestFactory mapRequestFactory,
        IComponentSelectorFactory componentSelectorFactory,
        IMappingResultFactory mappingResultFactory,
        IMapper<IMapRequest<IDocumentSection>, MappedResponse<GDSFieldsetComponent>> GdsfieldsetMapper,
        IMapper<IMapRequest<IDocumentSection>, MappedResponse<GDSButtonComponent>> GdsbuttonMapper,
        IMapper<IMapRequest<IDocumentSection>, MappedResponse<GDSTextInputComponent>> GdsTextInputMapper,
        IMapper<IMapRequest<IDocumentSection>, MappedResponse<GDSCheckboxComponent>> GdsCheckboxMapper,
        IMapper<IMapRequest<IDocumentSection>, MappedResponse<GDSRadioComponent>> GdsRadioMapper,
        IMapper<IMapRequest<IDocumentSection>, MappedResponse<GDSSelectComponent>> GdsSelectMapper,
        IMapper<IMapRequest<IDocumentSection>, MappedResponse<HiddenInputComponent>> hiddenInputFactory)
    {
        ArgumentNullException.ThrowIfNull(GdsfieldsetMapper);
        ArgumentNullException.ThrowIfNull(GdsbuttonMapper);
        ArgumentNullException.ThrowIfNull(GdsTextInputMapper);
        ArgumentNullException.ThrowIfNull(GdsCheckboxMapper);
        ArgumentNullException.ThrowIfNull(GdsRadioMapper);
        ArgumentNullException.ThrowIfNull(GdsSelectMapper);
        ArgumentNullException.ThrowIfNull(hiddenInputFactory);
        _componentSelectorFactory = componentSelectorFactory;
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
        IEnumerable<MappedResponse<GDSFieldsetComponent>> mappedFieldsets =
            request.FindManyDescendantsAndMap<GDSFieldsetComponent>(
                _mapRequestFactory,
                _componentSelectorFactory.GetSelector<GDSFieldsetComponent>(),
                _GdsfieldsetMapper)
            .AddMappedResponseToResults(request.MappingResults);

        IEnumerable<MappedResponse<GDSButtonComponent>> mappedButtons
            = request.FindManyDescendantsAndMap<GDSButtonComponent>(
                _mapRequestFactory,
                _componentSelectorFactory.GetSelector<GDSButtonComponent>(),
                _GdsButtonMapper)
            .AddMappedResponseToResults(request.MappingResults);

        IEnumerable<MappedResponse<HiddenInputComponent>> mappedHiddenInputs =
            request.FindManyDescendantsAndMap<HiddenInputComponent>(
                _mapRequestFactory,
                _componentSelectorFactory.GetSelector<HiddenInputComponent>(),
                _hiddenInputMapper)
        .AddMappedResponseToResults(request.MappingResults);

        IEnumerable<MappedResponse<GDSTextInputComponent>> mappedTextInputs
            = request.FindManyDescendantsAndMap<GDSTextInputComponent>(
                _mapRequestFactory,
                _componentSelectorFactory.GetSelector<GDSTextInputComponent>(),
                _GdsTextInputMapper)
        .AddMappedResponseToResults(request.MappingResults);

        // GDSCheckboxes
        IEnumerable<MappedResponse<GDSCheckboxComponent>> mappedCheckboxes
            = request.FindManyDescendantsAndMap<GDSCheckboxComponent>(
                _mapRequestFactory,
                _componentSelectorFactory.GetSelector<GDSCheckboxComponent>(),
                _GdsCheckboxMapper)
            .AddMappedResponseToResults(request.MappingResults);

        // GDSRadios
        IEnumerable<MappedResponse<GDSRadioComponent>> mappedRadios
            = request.FindManyDescendantsAndMap<GDSRadioComponent>(
                _mapRequestFactory,
                _componentSelectorFactory.GetSelector<GDSRadioComponent>(),
                _GdsRadioMapper)
            .AddMappedResponseToResults(request.MappingResults);

        // GDSSelects
        IEnumerable<MappedResponse<GDSSelectComponent>> mappedSelects
            = request.FindManyDescendantsAndMap<GDSSelectComponent>(
                _mapRequestFactory,
                _componentSelectorFactory.GetSelector<GDSSelectComponent>(),
                _GdsSelectMapper)
            .AddMappedResponseToResults(request.MappingResults);

        FormComponent form = new()
        {
            // form attributes
            // Default method is GET if not specified
            Method = HttpMethod.Parse(request.From.GetAttribute("method") ?? "get"),
            Action = request.From.GetAttribute("action") ?? string.Empty,
            IsFormValidated = !request.From.HasAttribute("novalidate"),
            // form parts
            FieldSets = mappedFieldsets.Select(t => t.Mapped),
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
            request.From);
    }
}
