namespace Dfe.Testing.Pages.Contracts;
public interface IMapper<TIn, TOut>
{
    TOut Map(TIn input);
}
