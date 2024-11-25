using Dfe.Testing.Pages.Components.Checkbox;
using Dfe.Testing.Pages.Components.Fieldset;
using Dfe.Testing.Pages.Internal.ComponentFactory;
using Dfe.Testing.Pages.Internal.ComponentFactory.Checkbox;

namespace Dfe.Testing.Pages.Internal.ComponentFactory.Fieldset;
internal sealed class GDSFieldsetMapper : IComponentMapper<GDSFieldsetComponent>
{
    private readonly GDSCheckboxWithLabelFactory _checkboxFactory;

    // TODO hard dependency on checkboxFactory from IDocumentPart - use scope instead or introduce IDocumentPart as part of Componentfactory?
    public GDSFieldsetMapper(IDocumentQueryClientAccessor accessor, IComponentMapper<GDSCheckboxWithLabelComponent> checkboxMapper)
    {
        ArgumentNullException.ThrowIfNull(accessor);
        _checkboxFactory = new GDSCheckboxWithLabelFactory(accessor, checkboxMapper);
    }
    public GDSFieldsetComponent Map(IDocumentPart input)
    {
        return new GDSFieldsetComponent()
        {
            TagName = input.TagName,
            Legend = input.GetChild(new CssSelector("legend"))?.Text ?? throw new ArgumentNullException("legend on fieldset is null"),
            Checkboxes = _checkboxFactory.GetCheckboxesFromPart(input)
        };
    }
}
