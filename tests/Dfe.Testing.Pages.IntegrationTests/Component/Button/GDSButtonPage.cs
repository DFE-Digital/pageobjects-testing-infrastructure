using Dfe.Testing.Pages.Public.Components;
using Dfe.Testing.Pages.Public.PageObject;
using Dfe.Testing.Pages.Shared.Selector;

namespace Dfe.Testing.Pages.IntegrationTests.Component.Button;
internal sealed class GDSButtonPage : IPageObject
{
    private readonly ComponentFactory<GDSButtonComponent> _buttonFactory;

    public GDSButtonPage(ComponentFactory<GDSButtonComponent> buttonFactory)
    {
        _buttonFactory = buttonFactory;
    }

    public GDSButtonComponent GetNoScope() => _buttonFactory.Create().Created;
    public GDSButtonComponent GetWithScope() => _buttonFactory.Create(new CreateComponentRequest()
    {
        FindInScope = new CssElementSelector("#gds-button-default-nested-nested-container")
    }).Created;
    public IEnumerable<GDSButtonComponent> GetManyNoScope() => _buttonFactory.CreateMany().Select(t => t.Created);
    public IEnumerable<GDSButtonComponent> GetManyWithScope() => _buttonFactory.CreateMany(new CreateComponentRequest()
    {
        FindInScope = new CssElementSelector("#gds-button-default-nested-container")
    }).Select(t => t.Created);
}
