using Dfe.Testing.Pages.Public.DocumentQueryClient.Pages;
using Dfe.Testing.Pages.Public.DocumentQueryClient.Pages.Components;
using Dfe.Testing.Pages.Public.DocumentQueryClient.Selector;

namespace Dfe.Testing.Pages.IntegrationTests.Pages;
internal sealed class GDSButtonPage : IPage
{
    private readonly ComponentFactory<GDSButton> _buttonFactory;

    public GDSButtonPage(ComponentFactory<GDSButton> buttonFactory)
    {
        _buttonFactory = buttonFactory;
    }

    public GDSButton GetDefaultGDSButton() => _buttonFactory.Get();
    public GDSButton GetDefaultGDSButtonWithScope() => _buttonFactory.Get(new QueryRequestArgs() { Scope = new CssSelector("#gds-button-default-nested-nested-container") });
    public IEnumerable<GDSButton> GetManyDefaultGDSButton() => _buttonFactory.GetMany();
    public IEnumerable<GDSButton> GetManyGDSButtonWithScope() => _buttonFactory.GetMany();
}
