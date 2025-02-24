using Dfe.Testing.Pages.Contracts.PageObjectClient.Request;
using Dfe.Testing.Pages.Contracts.PageObjectClient.Response;

namespace Dfe.Testing.Pages.Contracts.PageObjectClient;
public interface IPageObjectClient
{
    PageObjectResponse Get(PageObjectRequest request);
    // TODO should there be a builder that lets the client create a PageObjectRequest given it's complex
}
