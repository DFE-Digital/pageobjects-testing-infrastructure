using Dfe.Testing.Pages.Public.Components.GDS.ErrorMessage;
using Dfe.Testing.Pages.Public.Components.Inputs;
using Dfe.Testing.Pages.Public.Components.Label;

namespace Dfe.Testing.Pages.Public.Components.GDS.Radio;
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
        var radio = _radioFactory.GetManyFromPart(input).Single();
        return new()
        {
            Label = _labelFactory.GetManyFromPart(input).Single(),
            Radio = radio,
            ErrorMessage = _errorMessageFactory.GetManyFromPart(input).FirstOrDefault()
                ?? new GDSErrorMessageComponent() { ErrorMessage = string.Empty }
        };
    }
}
