using Dfe.Testing.Pages.Public.PageObjects.PageObjectClient.Request;

namespace Dfe.Testing.Pages.Public.PageObjects.PageObjectClient.Extensions;
public static class PageObjectSchemaExtensions
{
    public static PageObjectSchema UpdateSchema(
        this PageObjectSchema schema,
        string id,
        Action<PageObjectSchema>? updateHandler = null)
    {
        var resultSchema = FindPageObjectSchemaWithId(schema, id) ?? throw new ArgumentException($"Unable to find schema for {id}");
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
}
