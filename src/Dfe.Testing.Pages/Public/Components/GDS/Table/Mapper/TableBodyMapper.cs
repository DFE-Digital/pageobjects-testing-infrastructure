namespace Dfe.Testing.Pages.Public.Components.GDS.Table.Mapper;
internal class TableBodyMapper : BaseDocumentSectionToComponentMapper<TableBody>
{
    private readonly IMapper<IDocumentSection, TableRow> _rowMapper;

    public TableBodyMapper(
        IDocumentSectionFinder documentSectionFinder,
        IMapper<IDocumentSection, TableRow> rowMapper) : base(documentSectionFinder)
    {
        ArgumentNullException.ThrowIfNull(rowMapper);
        _rowMapper = rowMapper;
    }

    public override TableBody Map(IDocumentSection input)
    {
        IDocumentSection mappable = FindMappableSection<TableBody>(input);
        return new()
        {
            Rows = _documentSectionFinder.FindMany<TableRow>(mappable).Select(_rowMapper.Map)
        };
    }

    protected override bool IsMappableFrom(IDocumentSection section) => section.TagName.Equals("tbody", StringComparison.OrdinalIgnoreCase);
}
