using Dfe.Testing.Pages.Public.Components.Core.Text;

namespace Dfe.Testing.Pages.Public.Components.GDS.Table.Mapper;
internal sealed class GDSTableMapper : BaseDocumentSectionToComponentMapper<GDSTableComponent>
{
    private readonly IMapper<IDocumentSection, TextComponent> _textMapper;
    private readonly IMapper<IDocumentSection, TableHead> _tableHeadFactory;
    private readonly IMapper<IDocumentSection, TableBody> _tableBodyFactory;

    public GDSTableMapper(
        IDocumentSectionFinder documentSectionFinder,
        IMapper<IDocumentSection, TextComponent> textMapper,
        IMapper<IDocumentSection, TableHead> tableHeadMapper,
        IMapper<IDocumentSection, TableBody> tableBodyMapper) : base(documentSectionFinder)
    {
        ArgumentNullException.ThrowIfNull(textMapper);
        ArgumentNullException.ThrowIfNull(tableHeadMapper);
        ArgumentNullException.ThrowIfNull(tableBodyMapper);
        _textMapper = textMapper;
        _tableHeadFactory = tableHeadMapper;
        _tableBodyFactory = tableBodyMapper;
    }
    public override GDSTableComponent Map(IDocumentSection input)
    {
        var mappable = FindMappableSection<GDSTableComponent>(input);
        return new()
        {
            Heading = _documentSectionFinder.Find(mappable, new CssElementSelector("caption")).MapWith(_textMapper),
            Head = _documentSectionFinder.Find<TableHead>(input).MapWith(_tableHeadFactory),
            Body = _documentSectionFinder.Find<TableBody>(input).MapWith(_tableBodyFactory),
        };
    }

    protected override bool IsMappableFrom(IDocumentSection section) => section.TagName.Equals("table", StringComparison.OrdinalIgnoreCase);
}
