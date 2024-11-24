using Dfe.Testing.Pages.Public.DocumentQueryClient.Pages.Components;

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

    public override List<AnchorLink> GetMany(QueryRequestArgs? request = null)
         => DocumentQueryClient.QueryMany(
                args: MergeRequest(
                    request,
                    defaultFindBySelector: new CssSelector("a")),
                mapper: MapToLink).ToList();
}
