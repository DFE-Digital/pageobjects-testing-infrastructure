namespace Dfe.Testing.Pages.BrowserAdaptor.Contracts;
public sealed class BrowserOptions
{
    private readonly List<string> _flags = [];
    public int PageTimeoutSeconds { get; set; } = 60;
    public int CommandTimeoutSeconds { get; set; } = 65;
    public BrowserType Type { get; set; } = BrowserType.Chrome;
    public string DisplayHeight { get; set; } = "1080";
    public string DisplayWidth { get; set; } = "1920";
    public double? BrowserVersion { get; set; } = null;
    public bool EnableNetworkingMonitoring { get; set; } = false;
    public void AddDriverArgs(params string[] args)
    {
        ArgumentNullException.ThrowIfNull(args);
        args.Where(t => !string.IsNullOrEmpty(t))
            .ToList()
            .ForEach(_flags.Add);
    }
}
