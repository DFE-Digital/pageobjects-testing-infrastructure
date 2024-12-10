namespace Dfe.Testing.Pages.Internal.WebApplicationFactory;
internal class TestServerFactory<T> : WebApplicationFactory<T> where T : class
{
    private readonly IConfigureWebHostHandler _configureWebHostHandler;

    public TestServerFactory(IConfigureWebHostHandler configureWebHostHandler)
    {
        _configureWebHostHandler = configureWebHostHandler;
        // TODO options determine this, default to false
        ClientOptions.AllowAutoRedirect = true;
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        base.ConfigureWebHost(builder);
        _configureWebHostHandler.Handle(builder);
    }
}
