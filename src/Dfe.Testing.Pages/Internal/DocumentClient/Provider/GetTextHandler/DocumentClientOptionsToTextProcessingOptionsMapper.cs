using Dfe.Testing.Pages.Internal.DocumentClient.Options;
using Dfe.Testing.Pages.Internal.DocumentClient.Provider.GetTextHandler.Options;
using Dfe.Testing.Pages.Public.PageObjects;

namespace Dfe.Testing.Pages.Internal.DocumentClient.Provider.GetTextHandler;
internal class DocumentClientOptionsToTextProcessingOptionsMapper : IMapper<DocumentClientOptions, TextProcessingOptions>
{
    public TextProcessingOptions Map(DocumentClientOptions input)
    {
        return new TextProcessingOptions()
        {
            Trim = input.TrimText
        };
    }
}
