using Dfe.Testing.Pages.Components.ErrorMessage;
using Dfe.Testing.Pages.Components.Inputs.Radio;
using Dfe.Testing.Pages.Components.Label;
using Dfe.Testing.Pages.Public.Mapper.Abstraction;

namespace Dfe.Testing.Pages.Public.Mapper.GDS;
internal sealed class GDSRadioMapper : IComponentMapper<GDSRadioComponent>
{
    private readonly ComponentFactory<LabelComponent> _labelFactory;
    private readonly ComponentFactory<RadioComponent> _radioFactory;
    private readonly ComponentFactory<GDSErrorMessageComponent> _errorMessageFactory;

    public GDSRadioMapper(
        ComponentFactory<LabelComponent> labelFactory,
        ComponentFactory<RadioComponent> radioFactory,
        ComponentFactory<GDSErrorMessageComponent> errorMessageFactory)
    {
        _labelFactory = labelFactory;
        _radioFactory = radioFactory;
        _errorMessageFactory = errorMessageFactory;
    }
    public GDSRadioComponent Map(IDocumentPart input)
    {
        RadioComponent radio = _radioFactory.GetManyFromPart(input).Single();
        return new()
        {
            Label = _labelFactory.GetManyFromPart(input).Single(),
            Radio = radio,
            ErrorMessage = _errorMessageFactory.GetManyFromPart(input).FirstOrDefault()
                ?? new GDSErrorMessageComponent() { ErrorMessage = string.Empty }
        };
    }
}
