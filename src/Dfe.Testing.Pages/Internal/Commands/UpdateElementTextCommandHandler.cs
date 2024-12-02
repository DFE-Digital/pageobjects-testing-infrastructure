namespace Dfe.Testing.Pages.Internal.Commands;
internal sealed class UpdateElementTextCommandHandler : ICommandHandler<UpdateElementTextCommand>
{
    private readonly IDocumentQueryClientAccessor _documentQueryClientAccessor;

    public UpdateElementTextCommandHandler(IDocumentQueryClientAccessor documentQueryClientAccessor)
    {
        ArgumentNullException.ThrowIfNull(documentQueryClientAccessor, nameof(documentQueryClientAccessor));
        _documentQueryClientAccessor = documentQueryClientAccessor;
    }
    public void Handle(UpdateElementTextCommand command)
    {
        ArgumentNullException.ThrowIfNull(command);
        ArgumentNullException.ThrowIfNull(command.FindWith);

        _documentQueryClientAccessor.DocumentQueryClient.Run(
            new QueryOptions()
            {
                Query = command.FindWith,
                InScope = command.InScope
            },
            (part) =>
            {
                if (string.IsNullOrEmpty(command.Text))
                {
                    part.Text = string.Empty;
                    return;
                }
                part.Text = command.Text;
            });
    }
}
