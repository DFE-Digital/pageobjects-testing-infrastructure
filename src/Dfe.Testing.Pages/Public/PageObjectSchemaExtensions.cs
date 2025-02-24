
namespace Dfe.Testing.Pages.Public;
public static class PageObjectSchemaExtensions
{
    public static PageObjectSchema UpdateSchema(
        this PageObjectSchema schema,
        string id,
        Action<PageObjectSchema>? updateHandler = null)
    {
        PageObjectSchema? resultSchema = FindPageObjectSchemaWithId(schema, id) ?? throw new ArgumentException($"Unable to find schema for {id}");
        updateHandler?.Invoke(resultSchema);
        return schema;
    }

    private static PageObjectSchema? FindPageObjectSchemaWithId(PageObjectSchema schema, string id)
    {
        // iteratively search through the schema and children until the schema id is found
        if (schema is null)
        {
            return null;
        }

        if (string.IsNullOrEmpty(id))
        {
            return null;
        }

        if (schema.Id == id)
        {
            return schema;
        }

        foreach (var child in schema.Children)
        {
            var result = FindPageObjectSchemaWithId(child, id);
            if (result != null)
            {
                return result;
            }
        }
        return null;
    }

    public static IEnumerable<IDocumentSection> FindAllDocumentSectionsForPageObjectSchema(this IDocumentSection section, PageObjectSchema schema)
    {
        IEnumerable<IDocumentSection>? sections = string.IsNullOrEmpty(schema.Find) ?
            [section] :
                section.FindDescendants(new CssElementSelector(schema.Find));
        return sections ?? [];
    }
}
