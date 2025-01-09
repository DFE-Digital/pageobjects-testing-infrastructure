namespace Dfe.Testing.Pages.Public.PageObject;
public interface IPageObjectFactory
{
    TPage Create<TPage>() where TPage : class, IPageObject;
}
