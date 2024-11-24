using Dfe.Testing.Pages.Public.DocumentQueryClient.Components;

namespace Dfe.Testing.Pages.Internal.ComponentFactory;
internal sealed class AnchorLinkComponentFactory : ComponentFactory<AnchorLink>
{
    public AnchorLinkComponentFactory(IDocumentQueryClientAccessor documentQueryClientAccessor) : base(documentQueryClientAccessor)
    {
    }

    private static Func<IDocumentPart, AnchorLink> MapToLink =>
        (documentPart)
            => new AnchorLink
            {
                LinkValue = documentPart.GetAttribute("href")!,
                Text = documentPart.Text.Trim(),
                OpensInNewTab = documentPart.GetAttribute("target") == "_blank"
            };

    public override List<AnchorLink> GetMany(QueryRequest? request = null)
    {
        QueryRequest queryRequest = new()
        {
            Query = request?.Query ?? new CssSelector("a"),
            Scope = request?.Scope
        };

        return DocumentQueryClient.QueryMany(
            args: queryRequest,
            mapper: MapToLink).ToList();
    }
}
