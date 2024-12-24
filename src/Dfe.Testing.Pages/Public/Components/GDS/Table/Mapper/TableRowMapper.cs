namespace Dfe.Testing.Pages.Public.Components.GDS.Table.Mapper;
internal sealed class TableRowMapper : BaseDocumentSectionToComponentMapper<TableRow>
{
    private readonly IMapper<IDocumentSection, TableHeadingItem> _tableHeadingItemMapper;
    private readonly IMapper<IDocumentSection, TableDataItem> _tableDataItemMapper;

    public TableRowMapper(
        IDocumentSectionFinder documentSectionFinder,
        IMapper<IDocumentSection, TableHeadingItem> tableHeadingFactory,
        IMapper<IDocumentSection, TableDataItem> tableDataItemFactory) : base(documentSectionFinder)
    {
        ArgumentNullException.ThrowIfNull(tableHeadingFactory);
        ArgumentNullException.ThrowIfNull(tableDataItemFactory);
        _tableHeadingItemMapper = tableHeadingFactory;
        _tableDataItemMapper = tableDataItemFactory;
    }

    public override TableRow Map(IDocumentSection section)
    {
        var mappable = FindMappableSection<TableRow>(section);
        return new TableRow()
        {
            Headings = _documentSectionFinder.FindMany<TableHeadingItem>(mappable).Select(_tableHeadingItemMapper.Map),
            DataItem = _documentSectionFinder.FindMany<TableDataItem>(mappable).Select(_tableDataItemMapper.Map),
        };
    }

    protected override bool IsMappableFrom(IDocumentSection section) => section.TagName.Equals("tr", StringComparison.OrdinalIgnoreCase);
}
