using Dfe.Testing.Pages.Internal.DocumentClient.Options;
using Dfe.Testing.Pages.Public.WebDriver.Options;

namespace Dfe.Testing.Pages.Public.WebDriver;
public class WebDriverOptions : DocumentClientOptions
{
    public BrowserOptions Browser { get; set; } = new BrowserOptions();
}
