using Dfe.Testing.Pages.Public.Components.Core.Inputs;
using Dfe.Testing.Pages.Public.Components.Core.Label;
using Dfe.Testing.Pages.Public.Components.GDS.ErrorMessage;

namespace Dfe.Testing.Pages.Public.Components.GDS.TextInput;
internal sealed class GDSTextInputMapper : BaseDocumentSectionToComponentMapper<GDSTextInputComponent>
{
    private readonly IMapper<IDocumentSection, TextInputComponent> _textInputMapper;
    private readonly IMapper<IDocumentSection, LabelComponent> _labelMapper;
    private readonly IMapper<IDocumentSection, GDSErrorMessageComponent> _errorMessageMapper;

    public GDSTextInputMapper(
        IDocumentSectionFinder documentSectionFinder,
        IMapper<IDocumentSection, TextInputComponent> textInputMapper,
        IMapper<IDocumentSection, LabelComponent> labelMapper,
        IMapper<IDocumentSection, GDSErrorMessageComponent> errorMessageMapper) : base(documentSectionFinder)
    {
        ArgumentNullException.ThrowIfNull(textInputMapper);
        ArgumentNullException.ThrowIfNull(labelMapper);
        ArgumentNullException.ThrowIfNull(errorMessageMapper);
        _textInputMapper = textInputMapper;
        _labelMapper = labelMapper;
        _errorMessageMapper = errorMessageMapper;
    }

    public override GDSTextInputComponent Map(IDocumentSection input)
    {
        var mappable = FindMappableSection<GDSTextInputComponent>(input);
        return new GDSTextInputComponent()
        {
            Label = _labelMapper.Map(mappable),
            Input = _textInputMapper.Map(mappable),
            ErrorMessage = _errorMessageMapper.Map(mappable) ?? new GDSErrorMessageComponent() { ErrorMessage = new() { Text = string.Empty } }
        };
    }

    protected override bool IsMappableFrom(IDocumentSection section) => section.TagName.Equals("div", StringComparison.OrdinalIgnoreCase); // TODO && section.HasClass("govuk-form-group");
}
