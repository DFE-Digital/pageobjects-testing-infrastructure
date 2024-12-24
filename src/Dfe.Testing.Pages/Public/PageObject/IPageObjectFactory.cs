namespace Dfe.Testing.Pages.Public.PageObject;
public interface IPageObjectFactory
{
    TPage GetPage<TPage>() where TPage : class, IPageObject;
}
