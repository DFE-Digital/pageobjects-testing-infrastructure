using Dfe.Testing.Pages.IntegrationTests.Component.Helper;
using Dfe.Testing.Pages.Public.Components;
using Dfe.Testing.Pages.Public.Components.Form;
using Dfe.Testing.Pages.Public.Components.Link;

namespace Dfe.Testing.Pages.IntegrationTests.Component;
public sealed class GDSCookieBannerComponentTests
{

    [Fact]
    public async Task Should_Query_DefaultGDSCookieBanner()
    {
        // Arrange
        using var scope = ComponentTestHelper.GetServices<GDSCookieBannerPage>();
        var docService = scope.ServiceProvider.GetRequiredService<IDocumentService>();

        // Act
        await docService.RequestDocumentAsync(t => t.SetPath("/component/cookiebanner"));

        // Assert
        AnchorLinkComponent viewCookies = scope.ServiceProvider.GetRequiredService<IAnchorLinkComponentBuilder>()
            .SetText("View cookies")
            .SetLink("#")
            .SetOpensInNewTab(false)
            .Build();

        FormComponent form = scope.ServiceProvider.GetRequiredService<IFormBuilder>()
            .SetAction("/cookie-choice")
            .SetMethod("post")
            .AddButton(button => button.SetText("Accept additional cookies")
                .SetName("cookies[additional]")
                .SetType("submit")
                .SetValue("yes")
                .Build())
            .AddButton(button => button.SetText("Reject additional cookies")
                .SetName("cookies[additional]")
                .SetType("submit")
                .SetValue("no")
                .Build())
            .Build();

        GDSCookieChoiceAvailableBannerComponent expectedCookieChoiceBannerComponent =
            scope.ServiceProvider.GetRequiredService<IGDSCookieChoiceAvailableBannerComponentBuilder>()
                .SetForm(form)
                .SetHeading("Cookies on [name of service]")
                .SetViewCookiesLink(viewCookies)
                .Build();

        Assert.Equivalent(
            expectedCookieChoiceBannerComponent,
            scope.ServiceProvider.GetRequiredService<GDSCookieBannerPage>().GetBannerNoScope());
    }
}


internal sealed class GDSCookieBannerPage
{
    private readonly IComponentFactory<GDSCookieChoiceAvailableBannerComponent> _cookieBannerFactory;

    public GDSCookieBannerPage(IComponentFactory<GDSCookieChoiceAvailableBannerComponent> cookieBannerFactory)
    {
        ArgumentNullException.ThrowIfNull(cookieBannerFactory);
        _cookieBannerFactory = cookieBannerFactory;
    }

    internal GDSCookieChoiceAvailableBannerComponent GetBannerNoScope()
    {
        CreatedComponentResponse<GDSCookieChoiceAvailableBannerComponent> banner = _cookieBannerFactory.Create();
        return banner.Created;
    }
}
