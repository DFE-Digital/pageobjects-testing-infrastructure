using Dfe.Testing.Pages.Internal.DocumentClient;

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
            Selector = command.Selector,
            FindInScope = command.FindInScope
        };

        _documentService.ExecuteCommand(options,
            (part) =>
            {
                part.Text = string.IsNullOrEmpty(command.Text) ? string.Empty : command.Text;
            });
    }
}
