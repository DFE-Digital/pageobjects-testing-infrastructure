using Dfe.Testing.Pages.Public.Components.Core.Text;

namespace Dfe.Testing.Pages.Public.Components.GDS.Table.Mapper;
internal class TableDataItemMapper : BaseDocumentSectionToComponentMapper<TableDataItem>
{
    private readonly IMapper<IDocumentSection, TextComponent> _textMapper;

    public TableDataItemMapper(
        IMapper<IDocumentSection, TextComponent> textMapper,
        IDocumentSectionFinder documentSectionFinder) : base(documentSectionFinder)
    {
        ArgumentNullException.ThrowIfNull(textMapper);
        ArgumentNullException.ThrowIfNull(documentSectionFinder);
        _textMapper = textMapper;
    }

    public override TableDataItem Map(IDocumentSection section)
    {
        var mappable = FindMappableSection<TableDataItem>(section);
        return new()
        {
            Text = _textMapper.Map(mappable)
        };
    }

    protected override bool IsMappableFrom(IDocumentSection section) => section.TagName.Equals("td", StringComparison.OrdinalIgnoreCase);
}
