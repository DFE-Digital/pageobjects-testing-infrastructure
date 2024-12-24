using Dfe.Testing.Pages.Public.Components.Core.Label;
using Dfe.Testing.Pages.Public.Components.GDS.ErrorMessage;

namespace Dfe.Testing.Pages.Public.Components.GDS.Select;
internal sealed class GDSSelectMapper : BaseDocumentSectionToComponentMapper<GDSSelectComponent>
{
    private readonly IMapper<IDocumentSection, LabelComponent> _labelMapper;
    private readonly IMapper<IDocumentSection, OptionComponent> _optionFactory;
    private readonly IMapper<IDocumentSection, GDSErrorMessageComponent> _errorMessageFactory;

    public GDSSelectMapper(
        IDocumentSectionFinder documentSectionFinder,
        IMapper<IDocumentSection, LabelComponent> labelMapper,
        IMapper<IDocumentSection, OptionComponent> optionMapper,
        IMapper<IDocumentSection, GDSErrorMessageComponent> errorMessageMapper) : base(documentSectionFinder)
    {
        _labelMapper = labelMapper;
        _optionFactory = optionMapper;
        _errorMessageFactory = errorMessageMapper;
    }

    public override GDSSelectComponent Map(IDocumentSection input)
    {
        return new GDSSelectComponent()
        {
            Label = _documentSectionFinder.Find<LabelComponent>(input).MapWith(_labelMapper),
            Options = _documentSectionFinder.FindMany<OptionComponent>(input).Select(_optionFactory.Map),
            ErrorMessage =
                _documentSectionFinder.FindMany<GDSErrorMessageComponent>(input)
                    .FirstOrDefault()?.MapWith(_errorMessageFactory)
                        ?? new GDSErrorMessageComponent() { ErrorMessage = new() { Text = string.Empty } }
        };
    }

    protected override bool IsMappableFrom(IDocumentSection section) => section.TagName.Equals("select", StringComparison.OrdinalIgnoreCase);
}
