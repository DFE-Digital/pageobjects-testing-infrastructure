﻿namespace Dfe.Testing.Pages.Internal.WebDriver.Provider.Adaptor.Factory;

// TODO WHEN REMOTEDRIVER GRID - DriverService is only used for local driver executeable management, over a remote implementation not available build with variance in mind ... 
internal abstract class WebDriverFactoryBase<TDriver>
    where TDriver : IWebDriver
{
    public abstract Task<Func<TDriver>> CreateDriver(WebDriverSessionOptions sessionOptions);
}
