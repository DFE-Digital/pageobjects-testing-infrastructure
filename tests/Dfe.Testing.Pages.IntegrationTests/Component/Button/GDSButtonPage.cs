using Dfe.Testing.Pages.Public.Selector.Options;

namespace Dfe.Testing.Pages.IntegrationTests.Component.Button;
internal sealed class GDSButtonPage : IPageObject
{
    private readonly ComponentFactory<GDSButtonComponent> _buttonFactory;

    public GDSButtonPage(ComponentFactory<GDSButtonComponent> buttonFactory)
    {
        _buttonFactory = buttonFactory;
    }

    public GDSButtonComponent GetNoScope() => _buttonFactory.Get();
    public GDSButtonComponent GetWithScope() => _buttonFactory.Get(new QueryOptions()
    {
        InScope = new CssElementSelector("#gds-button-default-nested-nested-container")
    });
    public IEnumerable<GDSButtonComponent> GetManyNoScope() => _buttonFactory.GetMany();
    public IEnumerable<GDSButtonComponent> GetManyWithScope() => _buttonFactory.GetMany(new QueryOptions()
    {
        InScope = new CssElementSelector("#gds-button-default-nested-container")
    });
}
