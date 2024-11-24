# Who is this library for

To support .NET Developers and Testers in building Web application tests using composable PageObjects.

## What problems is the library solving

- Removing direct dependencies on specific test tools in PageModels and Tests and depending on this library to abstract common components and common query providers.

- Allows sharing components among different types of tests (Presentation layer tests, Integration Tests, EndToEnd UI tests) by substituting querying providers.

## Common terms and abstractions

- `IPage` - *Role:* mark pages with this
- `IPageFactory` - *Role:* create pages with this
- `IComponentFactory<IComponent>` *Role:* create components with this

## Using the library

The Dfe.Testing.Pages library supports the below providers

- `AngleSharp`

```cs
services.AddAngleSharp<TApplicationProgram>(); // TApplicationProgram is your .NET Program class for your Web Application
```

- `Selenium.WebDriver`

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

## Adding your own pages

When building PageModels you want to:

### Mark your pages with `IPage`

```cs
public sealed class MyPage : IPage

```

### Add your Page types into your tests DI

```cs
    services....
    services.AddTransient<IPage, HomePage>();
```

### Compose your PageModel of other PageComponents

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

    // Can reuse these across PageModels
    public NavigationBarComponent NavBar { get; }
    public SearchComponent Search { get; }
    public FilterComponent Filter { get; }
}
```

### Create your pages in your tests

Create *ALL* of your PageModels using the `IPageFactory`

```cs
public sealed class MyTestClass : BaseTest{

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
// Record to give value-object semantics
public record GDSTextInput
{
    public required string Name { get; init; }
    public required string Value { get; init; }
    public required string? PlaceHolder { get; init; } = null;
    public required string? Type { get; init; } = null;
}

GDSTextInput textInput = new()
{
    Name = "searchKeyWord",
    Value = "",
    PlaceHolder = "Search by keyword",
    Type = "text"
};

// Test
homePage.TextInput.Should().Be(textInput);

// Page
public sealed class HomePage
{
    
    public GDSTextInput  GetSearchInput() => _factoryForInputs_.Create();
}



```

```cs

// CUSTOM COMPLEX APPLICATION TYPE
public record Facet(string Name, IEnumerable<FacetValue> FacetValues);
public record FacetValue(string Label, string Value);

homePage.GetDisplayedFacets().Should().Be(new[]
{
    new Facet
    (
        Name: "Facet name",
        FacetValues: []
    ),
    new Facet
    (
        Name: "Facet name",
        FacetValues: []
    )
})

public sealed class HomePage
{
    public IEnumerable<Facet> GetDisplayedFacets()
        => _formFactory.Get().FieldSets
                .Select(
                    (fieldSet) => new Facet(
                        Name: fieldSet.Legend,
                        FacetValues: fieldSet.Checkboxes.Select(
                         (checkbox) => new FacetValue(checkbox.Label,   checkbox.Value))));
}
```
