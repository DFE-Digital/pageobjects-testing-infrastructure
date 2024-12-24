using Dfe.Testing.Pages.Public.Components.Core.Text;
using Dfe.Testing.Pages.Public.Components.GDS.Checkbox;
using Dfe.Testing.Pages.Public.Components.GDS.Radio;
using Dfe.Testing.Pages.Public.Components.GDS.TextInput;

namespace Dfe.Testing.Pages.Public.Components.GDS.Fieldset;
internal sealed class GDSFieldsetMapper : BaseDocumentSectionToComponentMapper<GDSFieldsetComponent>
{
    private readonly IMapper<IDocumentSection, GDSCheckboxComponent> _checkboxMapper;
    private readonly IMapper<IDocumentSection, GDSRadioComponent> _radioMapper;
    private readonly IMapper<IDocumentSection, GDSTextInputComponent> _textInputMapper;
    private readonly IMapper<IDocumentSection, GDSSelectComponent> _selectComponentMapper;
    private readonly IMapper<IDocumentSection, TextComponent> _textMapper;

    public GDSFieldsetMapper(
        IDocumentSectionFinder documentSectionFinder,
        IMapper<IDocumentSection, GDSCheckboxComponent> checkboxFactory,
        IMapper<IDocumentSection, GDSRadioComponent> radioFactory,
        IMapper<IDocumentSection, GDSTextInputComponent> textInput,
        IMapper<IDocumentSection, GDSSelectComponent> selectComponentFactory,
        IMapper<IDocumentSection, TextComponent> textMapper) : base(documentSectionFinder)
    {
        ArgumentNullException.ThrowIfNull(checkboxFactory);
        ArgumentNullException.ThrowIfNull(textInput);
        ArgumentNullException.ThrowIfNull(radioFactory);
        ArgumentNullException.ThrowIfNull(selectComponentFactory);
        ArgumentNullException.ThrowIfNull(textMapper);
        _checkboxMapper = checkboxFactory;
        _radioMapper = radioFactory;
        _textInputMapper = textInput;
        _selectComponentMapper = selectComponentFactory;
        _textMapper = textMapper;
    }
    public override GDSFieldsetComponent Map(IDocumentSection section)
    {
        var mappable = FindMappableSection<GDSFieldsetComponent>(section);
        return new GDSFieldsetComponent()
        {
            TagName = mappable.TagName,
            Legend = _documentSectionFinder.Find(mappable, new CssElementSelector("legend"))!.MapWith(_textMapper),
            TextInputs = _documentSectionFinder.FindMany<GDSTextInputComponent>(mappable).Select(t => t.MapWith(_textInputMapper)),
            Radios = _documentSectionFinder.FindMany<GDSRadioComponent>(mappable).Select(t => t.MapWith(_radioMapper)),
            Checkboxes = _documentSectionFinder.FindMany<GDSCheckboxComponent>(mappable).Select(t => t.MapWith(_checkboxMapper)),
        };
    }

    protected override bool IsMappableFrom(IDocumentSection section) => section.TagName.Equals("fieldset", StringComparison.OrdinalIgnoreCase);
}
