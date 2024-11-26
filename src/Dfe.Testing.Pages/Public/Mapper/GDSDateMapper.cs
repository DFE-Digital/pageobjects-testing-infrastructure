using Dfe.Testing.Pages.Components.Date;
using Dfe.Testing.Pages.Components.Fieldset;
using Dfe.Testing.Pages.Public.Mapper.Interface;

namespace Dfe.Testing.Pages.Public.Mapper;
internal sealed class GDSDateMapper : IComponentMapper<GDSDateComponent>
{
    private readonly ComponentFactory<GDSFieldsetComponent> _fieldsetFactory;

    public GDSDateMapper(ComponentFactory<GDSFieldsetComponent> fieldsetFactory)
    {
        ArgumentNullException.ThrowIfNull(fieldsetFactory);
        _fieldsetFactory = fieldsetFactory;
    }
    public GDSDateComponent Map(IDocumentPart input)
    {
        return new()
        {
            Fieldsets = _fieldsetFactory.GetMany()
        };
    }
}
