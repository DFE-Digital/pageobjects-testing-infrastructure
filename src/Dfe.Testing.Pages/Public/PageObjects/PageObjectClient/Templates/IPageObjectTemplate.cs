using Dfe.Testing.Pages.Public.PageObjects.PageObjectClient.Request;

namespace Dfe.Testing.Pages.Public.PageObjects.PageObjectClient.Templates;
// TODO consider Target<T> as an extension of this. Then it registers its type than id reflecting the type
public interface IPageObjectTemplate
{
    string Id { get; }
    PageObjectSchema Schema { get; }
}
