namespace Dfe.Testing.Pages.Public.Templates;
public record TabComponent
{
    public AnchorLinkComponent Tab { get; set; } = new();
}

internal sealed class TabComponentMapper : IMapper<CreatedPageObjectModel, IEnumerable<TabComponent>>
{
    public IEnumerable<TabComponent> Map(CreatedPageObjectModel input)
    {
        return input.GetMappedProperty("Tabs").Select(resolvedTabProperties =>
        {
            return new TabComponent()
            {
                Tab = new AnchorLinkComponent()
                {
                    Link = resolvedTabProperties.TryGetOrDefault("href"),
                    Text = resolvedTabProperties.TryGetOrDefault("text")
                }
            };
        });
    }
}
