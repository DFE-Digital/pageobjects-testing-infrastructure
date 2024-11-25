using Dfe.Testing.Pages.Components.Button;
using Dfe.Testing.Pages.Public.DocumentQueryClient;
using Dfe.Testing.Pages.Public.DocumentQueryClient.Selector;

namespace Dfe.Testing.Pages.IntegrationTests.Component.Button;
internal sealed class GDSButtonPage : IPage
{
    private readonly ComponentFactory<GDSButtonComponent> _buttonFactory;

    public GDSButtonPage(ComponentFactory<GDSButtonComponent> buttonFactory)
    {
        _buttonFactory = buttonFactory;
    }

    public GDSButtonComponent GetDefaultGDSButton() => _buttonFactory.Get();
    public GDSButtonComponent GetDefaultGDSButtonWithScope() => _buttonFactory.Get(new QueryRequestArgs() { Scope = new CssSelector("#gds-button-default-nested-nested-container") });
    public IEnumerable<GDSButtonComponent> GetManyDefaultGDSButton() => _buttonFactory.GetMany();
    public IEnumerable<GDSButtonComponent> GetManyGDSButtonWithScope() => _buttonFactory.GetMany();
}
