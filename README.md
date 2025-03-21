# Who is this library for

To support building .NET tests for web applications with composable PageObjects. This library targets usage of [GDS components](https://design-system.service.gov.uk/components/) in those applications.

Goals include;

- Provide testers with access to GDS components so they can be used as building blocks for PageObjects.

- Ensure different types of tests for a web application can use the same PageObjects.

- Ensure separation of concerns is followed closer between Test, PageObject, Document

## Getting started

1) Ensure your test project is using [dependency injection - DI](#setup-dependency-injection)

2) Choose a [query library](#choosing-and-configuring-a-library-to-query-for-you) for your test project

3) Register your [pages and components](#adding-your-own-pages) in your tests DI using `IPageObject`

4) Use [library provided Components](#components-available-to-use) in your pages with `ComponentFactory<TComponent>`

5) For further help see [docs](./docs/pageobjects-usecases.md) or reach out on dfe-slack  `public-pageobjects-testing-library`

## Choosing and configuring a library to query for you

### AngleSharp

```cs
// to use default AngleSharpOptions
services.AddAngleSharp(); 
services.AddWebApplicationFactory<TApplicationProgram>(); // TApplicationProgram is your Program class from your .NET Web Application

// to configure WebDriverOptions
services.AddAngleSharp(t => {

});
```

### WebDriver

```cs
// to use default WebDriverOptions
services.AddWebDriver();

// to configure WebDriverOptions
services.AddWebDriver(t => {
    t.Browser.BrowserName = "edge"; // browser being used
    t.Browser.BrowserMajorVersion = "131"; // version of browser
    t.Browser.ShowBrowser = false; // should the browser display a screen or be headless
    t.Browser.PageLoadTimeoutSeconds = 30; // how long should WebDriver wait for the page to load
    t.Browser.ViewportHeight = 1080; // the dimensions of the browser
    t.Browser.ViewportWidth = 1920; 
    t.Browser.EnableIncognito = false; // should the browser start as in incognito mode
    t.Browser.EnableAuthenticationBypass = false; // enable the network interception module
});

// TODO suggest to tester managing configuration through Binding? Provide a default JSON?
```

// TODO Update with CUSTOM MAPPING AND TEMPLATES 

## Components available to use

`IComponentFactory<TComponent>` *Role:* create components with this

```cs
// example of using library
public sealed class MyPage
{
    IComponentFactory<GDSHeaderComponent> _headerFactory
  // constructor
  public MyPage(IComponentFactory<GDSHeaderComponent> headerFactory)
  {
    _headerFactory = headerFactory;
  }

  // simplest usage of creating a GDSHeaderComponent
  public GDSHeaderComponent => _headerFactory.Create(... options).Created
}

```

## Adding your own pages

### Important! register your pages and application components in your DI

```cs
    testServices.AddTransient<HomePage>(); // page
    testServices.AddTransient<NavigationBarComponent>(); // application component

public sealed class HomePage
{
    public HomePage(
        NavigationBarComponent navBar,
        SearchComponent search)
    {
        NavBar = navBar ?? throw new ArgumentNullException(nameof(navBar));
        Search = search ?? throw new ArgumentNullException(nameof(search));
    }

    // Reuse these across Pages
    public NavigationBarComponent NavBar { get; }
    public SearchComponent Search { get; }
}
```

### Use pages in your tests

*Note* Ensure you have [Registered your pages and application components](#adding-your-own-pages)

```cs
public sealed class MyTestClass : BaseTest
{

    [Fact]
    public async Task MyTest()
    {
        // create document that the page will use
        IDocumentService documentService = GetTestService<IDocumentService>();
        
        // pageobjects use an document to fulfil their query requests
        await documentService.RequestDocumentAsync(
            (t) => t.SetPath("/"));

        // Resolve page through DI
        HomePage page = GetTestService<HomePage>();
    }
}

```

## Setup Dependency Injection

```cs
// This uses the Singleton pattern that wraps the DependencyInjection container ensuring a single instance of the container and, for services to be registered via `IServiceCollection`

internal sealed class DependencyInjection
{
    private static readonly DependencyInjection _instance = new();
    private readonly IServiceProvider _serviceProvider;
    static DependencyInjection()
    {
    }

    private DependencyInjection()
    {
        IServiceCollection services = new ServiceCollection()
            // ToAddAngleSharp .AddAngleSharp<Program>();
            // ToAddWebDriver .AddWebDriver();
            //
        services.AddTransient<IPageObject, ApplicationHomePage>();
        services.AddTransient<ApplicationComponent>();
        _serviceProvider = services.BuildServiceProvider();
    }

    public static DependencyInjection Instance
    {
        get
        {
            return _instance;
        }
    }

    internal IServiceScope CreateScope() => _serviceProvider.CreateScope();
}

// Separately you may want to make this Scope started and disposed in a base test class.

public abstract class BaseTest : IDisposable
{
    private readonly IServiceScope _serviceScope;

    protected BaseHttpTest()
    {
        _serviceScope = DependencyInjection.Instance.CreateScope();
    }

    protected T GetTestService<T>()
        => _serviceScope.ServiceProvider.GetService<T>()
            ?? throw new ArgumentNullException($"Unable to resolve type {typeof(T)}");

    public void Dispose()
    {
        GC.SuppressFinalize(this);
        _serviceScope.Dispose();
    }
}

// Any test class you inherit from BaseTest gets access to the container and a new scope is created per test.
// NOTE by inheriting - this does making overriding your container configuration not possible as the BaseTest is created (and it's ServiceProvider built) 
// before you can override parts of it in your test.

public sealed class MyTestClass : BaseTest
{
  [Fact]
  public async Task MyTest()
  {
    IDocumentSession documentSession = GetTestService<IDocumentSession>(); // is available
  }
}
```
