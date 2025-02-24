namespace Dfe.Testing.Pages.Public.PageObjects;
public interface IMapper<TIn, TOut>
{
    TOut Map(TIn input);
}
