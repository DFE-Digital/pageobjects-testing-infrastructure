using Dfe.Testing.Pages.Public.Components;
using Dfe.Testing.Pages.Public.Components.GDS.Header;
using Dfe.Testing.Pages.Public.PageObject;

namespace Dfe.Testing.Pages.IntegrationTests.Component.Header;
internal sealed class GDSHeaderPage : IPageObject
{
    private readonly ComponentFactory<GDSHeaderComponent> _headerFactory;

    public GDSHeaderPage(ComponentFactory<GDSHeaderComponent> headerFactory)
    {
        _headerFactory = headerFactory;
    }

    public GDSHeaderComponent GetHeaderWithNoScope() => _headerFactory.Get();
}

