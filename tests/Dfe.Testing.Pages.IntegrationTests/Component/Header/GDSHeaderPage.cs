using Dfe.Testing.Pages.Public.DocumentQueryClient.Pages;
using Dfe.Testing.Pages.Public.DocumentQueryClient.Pages.Components;

namespace Dfe.Testing.Pages.IntegrationTests.Component.Header;
internal sealed class GDSHeaderPage : IPage
{
    private readonly ComponentFactory<GDSHeader> _headerFactory;

    public GDSHeaderPage(ComponentFactory<GDSHeader> headerFactory)
    {
        _headerFactory = headerFactory;
    }

    public GDSHeader GetHeaderWithNoScope() => _headerFactory.Get();
}

