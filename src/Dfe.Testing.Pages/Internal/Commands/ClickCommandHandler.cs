using Dfe.Testing.Pages.Public.Commands;
using Dfe.Testing.Pages.Public.PageObjects.Documents;

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
            Find = command.Selector,
            InScope = command.FindInScope
        }, (part) => part.Click());
    }
}
