using Dfe.Testing.Pages.Public.Components;

namespace Dfe.Testing.Pages.Public;
internal interface IPageObjectClient
{
    PageObjectResponse Get(PageObjectRequest request);
    // TODO should there be a builder that lets the client create a PageObjectRequest given it's complex ... where client doesn't want to handle as JSON?
    // should we take in the schema, with JsonSerialiser overload - or force client to Serialise to model? if client wants YAML?
}
