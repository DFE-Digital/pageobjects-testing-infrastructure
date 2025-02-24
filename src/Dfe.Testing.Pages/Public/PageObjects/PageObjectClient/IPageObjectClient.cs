using Dfe.Testing.Pages.Public.PageObjects.PageObjectClient.Request;
using Dfe.Testing.Pages.Public.PageObjects.PageObjectClient.Response;

namespace Dfe.Testing.Pages.Public.PageObjects.PageObjectClient;
public interface IPageObjectClient
{
    PageObjectResponse Get(PageObjectRequest request);
    // TODO should there be a builder that lets the client create a PageObjectRequest given it's complex
}
