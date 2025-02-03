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
        using var scope = ComponentTestHelper.GetServices<GDSCookieBannerPage>();
        var docService = scope.ServiceProvider.GetRequiredService<IDocumentService>();
        await docService.RequestDocumentAsync(t => t.SetPath("/component/cookiebanner"));

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

        scope.ServiceProvider.GetRequiredService<GDSCookieBannerPage>().GetBannerNoScope()
            .Should()
                .BeEquivalentTo(expectedCookieChoiceBannerComponent);
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

    internal GDSCookieChoiceAvailableBannerComponent GetBannerNoScope() => _cookieBannerFactory.Create().Created;
}
