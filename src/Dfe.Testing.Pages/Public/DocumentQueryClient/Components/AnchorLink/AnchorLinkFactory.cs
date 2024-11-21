namespace Dfe.Testing.Pages.Public.DocumentQueryClient.Components.AnchorLink;
public sealed class AnchorLinkFactory : ComponentFactoryBase<AnchorLink>
{
    public AnchorLinkFactory(IDocumentQueryClientAccessor documentQueryClientAccessor) : base(documentQueryClientAccessor)
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
            mapper: MapToLink)
            .ToList();
    }
}
