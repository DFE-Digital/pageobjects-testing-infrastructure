namespace Dfe.Testing.Pages.Public.Components.GDS.Panel;
internal sealed class GDSPanelMapper : IComponentMapper<GDSPanelComponent>
{
    public GDSPanelComponent Map(IDocumentPart input)
    {
        return new()
        {
            Heading = input.FindDescendant(new CssElementSelector(".govuk-panel__title"))?.Text ?? throw new ArgumentNullException("panel has no heading"),
            Content = input.FindDescendant(new CssElementSelector(".govuk-panel__body"))?.Text ?? throw new ArgumentNullException("panel has no content")
        };
    }
}
