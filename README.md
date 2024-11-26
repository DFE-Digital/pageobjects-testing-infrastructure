# Who is this library for

To support .NET Developers and Testers in building Web application tests using composable PageObjects.

## What problems is the library solving

- Remove dependencies on specific test tools in PageObjects and Tests and depend on this library to 
  - abstract common GDS and generic components
  - abstract query providers.

- Allows sharing components among different types of tests
  - Presentation layer tests
  - Integration Tests
  - EndToEnd UI tests by substituting querying providers.

## Components available to use

`ComponentFactory<IComponent>` *Role:* create components with this

`IComponent` *Role:* mark components with this

### GDS components

- [Header](https://design-system.service.gov.uk/components/header/)
- [Button](https://design-system.service.gov.uk/components/button/)
- [Checkboxes](https://design-system.service.gov.uk/components/checkboxes/)
- [Fieldset](https://design-system.service.gov.uk/components/fieldset/)
- [CookieBanner](https://design-system.service.gov.uk/components/cookie-banner/)
- [TextInput](https://design-system.service.gov.uk/components/text-input)

### Generic components

- Anchor Link
- Form // TODO LINK

## Adding your own pages

When building PageModels you want to:

- `IPage` - *Role:* mark pages with this
- `IPageFactory` - *Role:* create pages with this

### Mark your pages with `IPage`

```cs
public sealed class MyPage : IPage

```

### Add your Page types into your test dependency injection

```cs
    testServices...
    testServices.AddTransient<IPage, HomePage>();
```

### Compose your PageComponents of other PageComponents

```cs

// NOTE make sure your register the components!
services.AddTransient<SearchComponent>();
services.AddTransient<FilterComponent>();

public sealed class HomePage
{
    public HomePage(
        NavigationBarComponent navBar,
        SearchComponent search, 
        FilterComponent filter)
    {
        Search = search ?? throw new ArgumentNullException(nameof(search));
        Filter = filter ?? throw new ArgumentNullException(nameof(filter));
        NavBar = navBar ?? throw new ArgumentNullException(nameof(navBar));
    }

    // Can reuse these across PageComponents
    public NavigationBarComponent NavBar { get; }
    public SearchComponent Search { get; }
    public FilterComponent Filter { get; }
}
```

### Create and use your pages in tests with `IPageFactory`

```cs
public sealed class MyTestClass : BaseTest
{

    [Fact]
    public async Task MyTest()
    {
        HttpRequestMessage homePageRequest = new()
        {
            Uri = new("/")
        }
        HomePage homePage = await GetTestService<IPageFactory>().CreatePageAsync<HomePage>(homePageRequest);
    }
}

```

### Expose types for your tests need without being coupled to a testing library

```cs
// Basic types example
homePage.GetHeading().Should().Be("Heading"); 

public sealed class HomePage
{
    public string GetHeading() => _headingFactory.GetHeadings().Where(t => t.Type == H1).Text;
}
```

```cs
// GDSComponent provided by the library
// record to give value-object semantics
public record GDSTextInput
{
    public required string Name { get; init; }
    public required string Value { get; init; }
    public required string? PlaceHolder { get; init; } = null;
    public required string? Type { get; init; } = null;
}

GDSTextInput expectedTextInput = new()
{
    Name = "searchKeyWord",
    Value = "",
    PlaceHolder = "Search by keyword",
    Type = "text"
};

// Test
homePage.GetSearchInput().Should().Be(expectedTextInput);

// Page
public sealed class HomePage
{
    
    public GDSTextInput GetSearchInput() => _factoryForInputs_.Create();
}



```

## Choosing a library to query for you

The library supports the below providers

### AngleSharp

```cs
services.AddAngleSharp<TApplicationProgram>(); // TApplicationProgram is your .NET Program class for your Web Application
```

### WebDriver

```cs
services.AddWebDriver();
```

In order to use the library you will need to setup [DependencyInjection](TODO LINK) inside of your tests and register the provider you want for that test suite. Below is an example of how you setup DependencyInjection.

```cs
// This uses the Singleton pattern that wraps the DependencyInjection container allowing for the services to be configured and built once. 
// The `IServiceProvider` once built, is delegated responsibility for creating registered implementations of types and managing their lifetimes.

// An `IServiceScope` is a child scope of the root DependencyInjection container, when you resolve through a scope, after you dispose of the scope - `Scoped` dependencies are disposed of.
.AddSingleton<TImplementation>()
.AddScoped<TInterface, TImplementation>();
_scope.Resolve<TInterface>();
_scope.Dispose(); // My scoped instance gets disposed, my Singleton remains

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
            .AddPages()
            // ToAddAngleSharp .AddAngleSharp<Program>();
            // ToAddWebDriver .AddWebDriver();
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

// Separately you want to consume this in your BaseTestClass

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

// Any test class you inherit from BaseTest gets access to the container and a new scope is created per test

public sealed class MyTestClass : BaseTest
{
  [Fact]
  public async Task MyTest()
  {
    IPageFactory pageFactory = GetTestService<IPageFactory>(); // is available
  }
}
```
