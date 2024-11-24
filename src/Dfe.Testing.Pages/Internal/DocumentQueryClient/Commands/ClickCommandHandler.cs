using Dfe.Testing.Pages.Public.Commands;

namespace Dfe.Testing.Pages.Internal.DocumentQueryClient.Commands;
internal sealed class ClickElementCommandHandler : ICommandHandler<ClickElementCommand>
{
    private readonly IDocumentQueryClientAccessor _documentQueryClientAccessor;

    public ClickElementCommandHandler(IDocumentQueryClientAccessor documentQueryClientAccessor)
    {
        _documentQueryClientAccessor = documentQueryClientAccessor;
    }
    public void Handle(ClickElementCommand command)
    {
        ArgumentNullException.ThrowIfNull(command);
        ArgumentNullException.ThrowIfNull(command.FindWith);
        _documentQueryClientAccessor.DocumentQueryClient.Run(
            new QueryRequestArgs()
            {
                Query = command.FindWith,
                Scope = command.InScope
            },
            (part) => part.Click());
    }
}
