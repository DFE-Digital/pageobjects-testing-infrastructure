namespace Dfe.Testing.Pages.Internal.Extensions;
internal static class DocumentSectionMapperExtensions
{
    internal static T MapWith<T>(this IDocumentSection section, IMapper<IDocumentSection, T> mapper)
    {
        ArgumentNullException.ThrowIfNull(section);
        ArgumentNullException.ThrowIfNull(mapper);
        return mapper.Map(section);
    }

    internal static IEnumerable<T> MapWith<T>(this IEnumerable<IDocumentSection> section, IMapper<IDocumentSection, T> mapper)
    {
        ArgumentNullException.ThrowIfNull(section);
        return section.Select(t => t.MapWith(mapper));
    }
}
