using Dfe.Testing.Pages.Public.Components.Core.Inputs;
using Dfe.Testing.Pages.Public.Components.GDS.Button;
using Dfe.Testing.Pages.Public.Components.GDS.Checkbox;
using Dfe.Testing.Pages.Public.Components.GDS.Fieldset;
using Dfe.Testing.Pages.Public.Components.GDS.Radio;
using Dfe.Testing.Pages.Public.Components.GDS.TextInput;
using HttpMethod = System.Net.Http.HttpMethod;

namespace Dfe.Testing.Pages.Public.Components.Core.Form;
internal sealed class FormMapper : BaseDocumentSectionToComponentMapper<FormComponent>
{
    private readonly IMapper<IDocumentSection, GDSFieldsetComponent> _GdsfieldsetMapper;
    private readonly IMapper<IDocumentSection, GDSButtonComponent> _GdsButtonMapper;
    private readonly IMapper<IDocumentSection, GDSTextInputComponent> _GdsTextInputMapper;
    private readonly IMapper<IDocumentSection, GDSCheckboxComponent> _GdsCheckboxMapper;
    private readonly IMapper<IDocumentSection, GDSRadioComponent> _GdsRadioMapper;
    private readonly IMapper<IDocumentSection, GDSSelectComponent> _GdsSelectMapper;
    private readonly IMapper<IDocumentSection, HiddenInputComponent> _hiddenInputMapper;

    public FormMapper(
        IDocumentSectionFinder documentSectionFinder,
        IMapper<IDocumentSection, GDSFieldsetComponent> GdsfieldsetMapper,
        IMapper<IDocumentSection, GDSButtonComponent> GdsbuttonMapper,
        IMapper<IDocumentSection, GDSTextInputComponent> GdsTextInputMapper,
        IMapper<IDocumentSection, GDSCheckboxComponent> GdsCheckboxMapper,
        IMapper<IDocumentSection, GDSRadioComponent> GdsRadioMapper,
        IMapper<IDocumentSection, GDSSelectComponent> GdsSelectMapper,
        IMapper<IDocumentSection, HiddenInputComponent> hiddenInputFactory) : base(documentSectionFinder)
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
    }

    public override FormComponent Map(IDocumentSection input)
    {
        var mappable = FindMappableSection<FormComponent>(input);
        return new FormComponent()
        {
            // form attributes
            Method = HttpMethod.Parse(
                mappable.GetAttribute("method") ?? throw new ArgumentNullException(nameof(FormComponent.Method), "method on form is null")),
            Action = mappable.GetAttribute("action") ?? string.Empty,
            IsFormValidated = !mappable.HasAttribute("novalidate"),
            // form parts
            FieldSets = _documentSectionFinder.FindMany<GDSFieldsetComponent>(mappable).MapWith(_GdsfieldsetMapper),
            Buttons = _documentSectionFinder.FindMany<GDSButtonComponent>(mappable).MapWith(_GdsButtonMapper),
            HiddenInputs = _documentSectionFinder.FindMany<HiddenInputComponent>(mappable).MapWith(_hiddenInputMapper),
            TextInputs = _documentSectionFinder.FindMany<GDSTextInputComponent>(mappable).MapWith(_GdsTextInputMapper),
            Checkboxes = _documentSectionFinder.FindMany<GDSCheckboxComponent>(mappable).MapWith(_GdsCheckboxMapper),
            Radios = _documentSectionFinder.FindMany<GDSRadioComponent>(mappable).MapWith(_GdsRadioMapper),
            Selects = _documentSectionFinder.FindMany<GDSSelectComponent>(mappable).MapWith(_GdsSelectMapper),
        };
    }

    protected override bool IsMappableFrom(IDocumentSection part) => part.TagName == "form";
}
