using Dfe.Testing.Pages.Public.Commands;
using Dfe.Testing.Pages.Public.Selector.Options;

namespace Dfe.Testing.Pages.Internal.Commands;
internal sealed class ClickElementCommandHandler : ICommandHandler<ClickElementCommand>
{
    private readonly IDocumentQueryClientAccessor _documentQueryClientAccessor;

    public ClickElementCommandHandler(IDocumentQueryClientAccessor documentQueryClientAccessor)
    {
        ArgumentNullException.ThrowIfNull(documentQueryClientAccessor, nameof(documentQueryClientAccessor));
        _documentQueryClientAccessor = documentQueryClientAccessor;
    }
    public void Handle(ClickElementCommand command)
    {
        ArgumentNullException.ThrowIfNull(command);
        ArgumentNullException.ThrowIfNull(command.FindWith);
        _documentQueryClientAccessor.DocumentQueryClient.Run(
            new QueryOptions()
            {
                Query = command.FindWith,
                InScope = command.InScope
            },
            (part) => part.Click());
    }
}
