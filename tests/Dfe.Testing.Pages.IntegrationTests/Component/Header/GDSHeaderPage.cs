using Dfe.Testing.Pages.Components.Header;
using Dfe.Testing.Pages.Public.DocumentQueryClient;

namespace Dfe.Testing.Pages.IntegrationTests.Component.Header;
internal sealed class GDSHeaderPage : IPage
{
    private readonly ComponentFactory<GDSHeaderComponent> _headerFactory;

    public GDSHeaderPage(ComponentFactory<GDSHeaderComponent> headerFactory)
    {
        _headerFactory = headerFactory;
    }

    public GDSHeaderComponent GetHeaderWithNoScope() => _headerFactory.Get();
}

