using Dfe.Testing.Pages.Public.Components.Core.Inputs;
using Dfe.Testing.Pages.Public.Components.Core.Label;

namespace Dfe.Testing.Pages.Public.Components.GDS.Checkbox;
internal sealed class GDSCheckboxMapper : BaseDocumentSectionToComponentMapper<GDSCheckboxComponent>
{
    private readonly IMapper<IDocumentSection, CheckboxComponent> _checkboxMapper;
    private readonly IMapper<IDocumentSection, LabelComponent> _labelMapper;

    public GDSCheckboxMapper(
        IDocumentSectionFinder documentSectionFinder,
        IMapper<IDocumentSection, CheckboxComponent> checkboxMapper,
        IMapper<IDocumentSection, LabelComponent> labelMapper)
            : base(documentSectionFinder)
    {
        _checkboxMapper = checkboxMapper;
        _labelMapper = labelMapper;
    }

    public override GDSCheckboxComponent Map(IDocumentSection section) // MapComponentRequest<TComponent> ComponentFinderMapping, 
    {
        IDocumentSection mappable = FindMappableSection<GDSCheckboxComponent>(section);
        CheckboxComponent checkbox = _checkboxMapper.Map(mappable);
        return new()
        {
            Label = _labelMapper.Map(mappable),
            Name = checkbox.Name,
            Value = checkbox.Value,
            Checked = checkbox.IsChecked,
            IsRequired = checkbox.IsRequired
        };
    }

    protected override bool IsMappableFrom(IDocumentSection part) => true; // TODO something with input
}
