namespace Dfe.Testing.Pages.Shared.Contracts;
public interface ICommandHandler<TCommand> where TCommand : ICommand
{
    public void Handle(TCommand command);
}
