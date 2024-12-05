namespace Dfe.Testing.Pages.Public.Components.GDS.ErrorMessage;
internal sealed class GDSErrorMessageMapper : IComponentMapper<GDSErrorMessageComponent>
{
    public GDSErrorMessageComponent Map(IDocumentPart input)
    {
        return new()
        {
            ErrorMessage = input.FindDescendant(new CssElementSelector(".govuk-error-message"))?.Text ?? string.Empty
        };
    }
}
