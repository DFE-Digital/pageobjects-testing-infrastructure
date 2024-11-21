namespace Dfe.Testing.Pages.Public.WebApplicationFactory;
public interface IConfigureWebHostHandler
{
    IWebHostBuilder Handle(IWebHostBuilder builder);
    void ConfigureWith(Action<IWebHostBuilder> configure);
}