namespace Dfe.Testing.Pages.Public.Components.GDS.Table.Mapper;
internal class TableHeadMapper : BaseDocumentSectionToComponentMapper<TableHead>
{
    private readonly IMapper<IDocumentSection, TableRow> _rowMapper;

    public TableHeadMapper(
        IDocumentSectionFinder documentSectionFinder,
        IMapper<IDocumentSection, TableRow> rowMapper) : base(documentSectionFinder)
    {
        ArgumentNullException.ThrowIfNull(rowMapper);
        _rowMapper = rowMapper;
    }

    public override TableHead Map(IDocumentSection section)
    {
        var mappable = FindMappableSection<TableHead>(section);
        return new()
        {
            Rows = _documentSectionFinder.FindMany<TableRow>(mappable).Select(_rowMapper.Map)
        };
    }

    protected override bool IsMappableFrom(IDocumentSection section) => section.TagName.Equals("thead", StringComparison.OrdinalIgnoreCase);
}
