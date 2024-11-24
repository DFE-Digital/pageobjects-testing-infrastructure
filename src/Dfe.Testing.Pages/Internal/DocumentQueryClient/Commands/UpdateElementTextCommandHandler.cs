using Dfe.Testing.Pages.Public.Commands;

namespace Dfe.Testing.Pages.Internal.DocumentQueryClient.Commands;
internal sealed class UpdateElementTextCommandHandler : ICommandHandler<UpdateElementTextCommand>
{
    private readonly IDocumentQueryClientAccessor _documentQueryClientAccessor;

    public UpdateElementTextCommandHandler(IDocumentQueryClientAccessor documentQueryClientAccessor)
    {
        _documentQueryClientAccessor = documentQueryClientAccessor;
    }
    public void Handle(UpdateElementTextCommand command)
    {
        throw new NotImplementedException();
    }
}
