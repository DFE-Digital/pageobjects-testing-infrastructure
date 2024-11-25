using Dfe.Testing.Pages.Components.Checkbox;
using Dfe.Testing.Pages.Components.Fieldset;

namespace Dfe.Testing.Pages.Internal.Mapper;
internal sealed class GDSFieldsetMapper : IComponentMapper<GDSFieldsetComponent>
{
    private readonly GDSCheckboxFactory _checkboxFactory;

    // TODO hard dependency on checkboxFactory from IDocumentPart - use scope instead or introduce IDocumentPart as part of Componentfactory?
    public GDSFieldsetMapper(IComponentSelectorFactory componentSelectorFactory, IDocumentQueryClientAccessor accessor, IComponentMapper<GDSCheckboxComponent> checkboxMapper)
    {
        ArgumentNullException.ThrowIfNull(accessor);
        _checkboxFactory = new GDSCheckboxFactory(componentSelectorFactory, accessor, checkboxMapper);
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
