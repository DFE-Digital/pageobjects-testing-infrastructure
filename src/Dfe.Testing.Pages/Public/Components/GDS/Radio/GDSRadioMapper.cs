using Dfe.Testing.Pages.Public.Components.Core.Inputs;
using Dfe.Testing.Pages.Public.Components.Core.Label;
using Dfe.Testing.Pages.Public.Components.GDS.ErrorMessage;

namespace Dfe.Testing.Pages.Public.Components.GDS.Radio;
internal sealed class GDSRadioMapper : BaseDocumentSectionToComponentMapper<GDSRadioComponent>
{
    private readonly IMapper<IDocumentSection, LabelComponent> _labelMapper;
    private readonly IMapper<IDocumentSection, RadioComponent> _radioMapper;
    private readonly IMapper<IDocumentSection, GDSErrorMessageComponent> _errorMessageMapper;

    public GDSRadioMapper(
        IDocumentSectionFinder documentSectionFinder,
        IMapper<IDocumentSection, LabelComponent> labelMapper,
        IMapper<IDocumentSection, RadioComponent> radioMapper,
        IMapper<IDocumentSection, GDSErrorMessageComponent> errorMessageMapper) : base(documentSectionFinder)
    {
        _labelMapper = labelMapper;
        _radioMapper = radioMapper;
        _errorMessageMapper = errorMessageMapper;
    }
    public override GDSRadioComponent Map(IDocumentSection section)
    {
        var mappable = FindMappableSection<GDSRadioComponent>(section);
        return new()
        {
            Label = _documentSectionFinder.FindMany<LabelComponent>(mappable).Single().MapWith(_labelMapper),
            Radio = _documentSectionFinder.FindMany<RadioComponent>(mappable).Single().MapWith(_radioMapper),
            ErrorMessage = _documentSectionFinder.FindMany<GDSErrorMessageComponent>(mappable).Select(_errorMessageMapper.Map).FirstOrDefault() ?? new GDSErrorMessageComponent()
            {
                ErrorMessage = new()
                {
                    Text = string.Empty
                }
            }
        };
    }

    protected override bool IsMappableFrom(IDocumentSection section) => true; // TODO contains radio
}
