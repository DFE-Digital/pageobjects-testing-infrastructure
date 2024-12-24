using Dfe.Testing.Pages.Public.Components.Core.Text;

namespace Dfe.Testing.Pages.Public.Components.GDS.Table.Mapper;
internal class TableHeadingMapper : BaseDocumentSectionToComponentMapper<TableHeadingItem>
{
    private readonly IMapper<IDocumentSection, TextComponent> _textMapper;

    public TableHeadingMapper(
        IDocumentSectionFinder documentSectionFinder,
        IMapper<IDocumentSection, TextComponent> textMapper) : base(documentSectionFinder)
    {
        ArgumentNullException.ThrowIfNull(documentSectionFinder);
        ArgumentNullException.ThrowIfNull(textMapper);
        _textMapper = textMapper;
    }
    public override TableHeadingItem Map(IDocumentSection input)
    {
        IDocumentSection mappable = FindMappableSection<TableHeadingItem>(input);
        return new()
        {
            Scope = mappable.GetAttribute("scope") ?? string.Empty,
            Text = _textMapper.Map(mappable)
        };
    }

    protected override bool IsMappableFrom(IDocumentSection section) => section.TagName.Equals("th", StringComparison.OrdinalIgnoreCase);

}
