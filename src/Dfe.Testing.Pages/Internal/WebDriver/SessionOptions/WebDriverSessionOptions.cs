﻿namespace Dfe.Testing.Pages.Internal.WebDriver.SessionOptions;
internal sealed class WebDriverSessionOptions
{
    public BrowserType BrowserType { get; set; }
    public TimeSpan PageLoadTimeout { get; set; }
    public TimeSpan RequestTimeout { get; set; }
    public bool IsNetworkInterceptionEnabled { get; set; } = false;
    public bool IsHeadless { get; set; } = false;
    // TODO should the options be a list or dict<list> mapping? { chrome: { ... }, { edge: { ... }, {default: {...}
    public IDictionary<BrowserType, IEnumerable<string>> BrowserOptions { get; set; } = new Dictionary<BrowserType, IEnumerable<string>>();
    public IReadOnlyCollection<string> Options { get; set; } = [];
}
