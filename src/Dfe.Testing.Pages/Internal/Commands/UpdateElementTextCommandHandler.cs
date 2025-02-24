using Dfe.Testing.Pages.Contracts.Documents;
using Dfe.Testing.Pages.Internal.DocumentClient;
using Dfe.Testing.Pages.Public.Commands;

namespace Dfe.Testing.Pages.Internal.Commands;
internal sealed class UpdateElementTextCommandHandler : ICommandHandler<UpdateElementTextCommand>
{
    private readonly IDocumentService _documentService;

    public UpdateElementTextCommandHandler(IDocumentService documentService)
    {
        _documentService = documentService;
    }
    public void Handle(UpdateElementTextCommand command)
    {
        ArgumentNullException.ThrowIfNull(command);
        ArgumentNullException.ThrowIfNull(command.Selector);

        FindOptions options = new FindOptions()
        {
            Find = command.Selector,
            InScope = command.FindInScope
        };

        _documentService.ExecuteCommand(options,
            (part) =>
            {
                part.Text = string.IsNullOrEmpty(command.Text) ? string.Empty : command.Text;
            });
    }
}
