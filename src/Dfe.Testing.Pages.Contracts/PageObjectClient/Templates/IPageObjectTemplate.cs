using Dfe.Testing.Pages.Contracts.PageObjectClient.Request;

namespace Dfe.Testing.Pages.Contracts.PageObjectClient.Templates;
// TODO consider Target<T> as an extension of this. Then it registers its type than id reflecting the type
public interface IPageObjectTemplate
{
    string Id { get; }
    PageObjectSchema Schema { get; }
}
