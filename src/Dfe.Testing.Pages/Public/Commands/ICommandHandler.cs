namespace Dfe.Testing.Pages.Public.Commands;
public interface ICommandHandler<TCommand> where TCommand : ICommand
{
    public void Handle(TCommand command);
}
