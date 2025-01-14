namespace Dfe.Testing.Pages.Internal.DocumentClient.Options;

public class DocumentClientOptions
{
    // TODO TextProcessingOptions : { Handlers : [ "Handler" : handlerName, "Enabled" : true/false] } 
    public bool TrimText { get; set; } = true;
    public int RequestTimeoutSeconds { get; set; } = 30;
}
