namespace Dfe.Testing.Pages.Shared.Contracts;
public interface IMapper<TIn, TOut>
{
    TOut Map(TIn input);
}
