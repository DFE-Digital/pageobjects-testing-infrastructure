using Dfe.Testing.Pages.Internal.DocumentClient;
using Dfe.Testing.Pages.Public.Commands;

namespace Dfe.Testing.Pages.Internal.Commands;
internal sealed class ClickElementCommandHandler : ICommandHandler<ClickElementCommand>
{
    private readonly IDocumentService _documentService;

    public ClickElementCommandHandler(IDocumentService documentClient)
    {
        _documentService = documentClient;
    }
    public void Handle(ClickElementCommand command)
    {
        ArgumentNullException.ThrowIfNull(command);
        ArgumentNullException.ThrowIfNull(command.Selector);

        _documentService.ExecuteCommand(new FindOptions()
        {
            Selector = command.Selector,
            FindInScope = command.FindInScope
        }, (part) => part.Click());
    }
}
