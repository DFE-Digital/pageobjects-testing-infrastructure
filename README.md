# Who is this library for

To support .NET Developers and Testers in building Web application tests with composable PageObjects;

- Removing PageObject and Test references to external query libraries with access to them behind an abstraction. (*e.g* WebDriver, AngleSharp... )

- Provide common GDS components with default mappings

- Encourage composition of PageObjects so that different types of tests can use the same PageObjects.

## Getting started

1) Ensure your test project is using [dependency injection - DI](#setup-dependency-injection)

2) Choose a [query library](#choosing-and-configuring-a-library-to-query-for-you) for your test project

3) Register your [pages and components](#adding-your-own-pages) in your tests DI using `IPageObject`

4) Use [library provided Components](#components-available-to-use) in your pages with `ComponentFactory<TComponent>`

5) Access pages [in your tests](#use-pages-in-your-tests) with `IDocumentSessionClient`

## Choosing and configuring a library to query for you

// TODO document configuration options for each provider; WebDriverSession, AngleSharpParseOptions (AlwaysUseContext, TextReplacement) - needs to be decoratable for retrypolicy

### AngleSharp

**TODO** separate document creator from query so AngleSharp can be used independently from WebAppFactory.

```cs
services.AddAngleSharp<TApplicationProgram>(); // TApplicationProgram is your .NET Program class for your Web Application

```

### WebDriver

```cs
services.AddWebDriver();
```

## Components available to use

`ComponentFactory<IComponent>` *Role:* create components with this
`IComponent` *Role:* marks components available

```cs
// example of using library
public sealed class MyPage : IPageObject
{
    ComponentFactory<HeaderComponent> _headerFactory
  // constructor
  public MyPage(ComponentFactory<HeaderComponent> headerFactory)
  {
    _headerFactory = headerFactory;
  }

  // simplest usage exposing the library type to tests
  public HeaderComponent => _headerFactory.Get(... options)
}

```

The library supports below components (GDS and supporting *e.g* AnchorLink `<a>`)

GDS Header

- [GDS](https://design-system.service.gov.uk/components/header/)
- [Library](/src/Dfe.Testing.Pages/Public/Components/GDS/Header/GDSHeaderComponent.cs)

GDS Footer

- [GDS](https://design-system.service.gov.uk/components/footer/)
- [Library](/src/Dfe.Testing.Pages/Public/Components/GDS/Footer/GDSFooterComponent.cs)

GDS Button

- [GDS](https://design-system.service.gov.uk/components/button/)
- [Library](/src/Dfe.Testing.Pages/Public/Components/GDS/Button/GDSButtonComponent.cs)

GDS Checkboxes

- [GDS](https://design-system.service.gov.uk/components/checkboxes/)
- [Library component](/src/Dfe.Testing.Pages/Public/Components/GDS/Checkbox/GDSCheckboxComponent.cs)

GDS Radio

- [GDS](https://design-system.service.gov.uk/components/radios/)
- [Library](/src/Dfe.Testing.Pages/Public/Components/GDS/Radio/GDSRadioComponent.cs)

GDS Fieldset

- [GDS](https://design-system.service.gov.uk/components/fieldset/)
- [Library](/src/Dfe.Testing.Pages/Public/Components/GDS/Fieldset/GDSFieldsetComponent.cs)

GDS CookieBanner

- [GDS](https://design-system.service.gov.uk/components/cookie-banner/)
- [Library](/src/Dfe.Testing.Pages/Public/Components/GDS/CookieBanner/GDSCookieBannerComponent.cs)

GDS TextInput

- [GDS](https://design-system.service.gov.uk/components/text-input)
- [Library](/src/Dfe.Testing.Pages/Public/Components/GDS/TextInput/GDSTextInputComponent.cs)

GDS DateInput

- [GDS](https://design-system.service.gov.uk/components/date-input)
- Library use Fieldset

GDS Tabs

- [GDS](https://design-system.service.gov.uk/components/tabs/)
- [Library](/src/Dfe.Testing.Pages/Public/Components/GDS/Tabs/GDSTabsComponent.cs)

GDS Details

- [GDS](https://design-system.service.gov.uk/components/details/)
- [Library](/src/Dfe.Testing.Pages/Public/Components/GDS/Details/GDSDetailsComponent.cs)

GDS ErrorMessage

- [GDS](https://design-system.service.gov.uk/components/error-message/)
- [Library](/src/Dfe.Testing.Pages/Public/Components/GDS/ErrorMessage/GDSErrorMessageComponent.cs)

GDS ErrorSummary

- [GDS](https://design-system.service.gov.uk/components/error-summary/)
- [Library](/src/Dfe.Testing.Pages/Public/Components/GDS/ErrorSummary/GDSErrorSummaryComponent.cs)

GDS Pagination

- [GDS](https://design-system.service.gov.uk/components/pagination/)
- Library use AnchorLink

GDS Panel

- [GDS](https://design-system.service.gov.uk/components/panel/)
- [Library](/src/Dfe.Testing.Pages/Public/Components/GDS/Panel/GDSPanelComponent.cs)

GDS Select

- [GDS](https://design-system.service.gov.uk/components/select/)
- [Library](/src/Dfe.Testing.Pages/Public/Components/GDS/Select/GDSSelectComponent.cs)

GDS Table

- [GDS](https://design-system.service.gov.uk/components/table/)
- [Library](/src/Dfe.Testing.Pages/Public/Components/GDS/Table/GDSTableComponent.cs)

- GDS Password TODO
- GDS Tag TODO

---END GDS---

Anchor Link

- [Library](/src/Dfe.Testing.Pages/Public/Components/Link/AnchorLinkComponent.cs)

Form

- [Library](/src/Dfe.Testing.Pages/Public/Components/Form/FormComponent.cs)

Label

- [Library](/src/Dfe.Testing.Pages/Public/Components/Label/LabelComponent.cs)

Option

- [Library](/src/Dfe.Testing.Pages/Public/Components/GDS/Select/OptionComponent.cs)

Table parts

- [Library TableHead](/src/Dfe.Testing.Pages/Public/Components/GDS/Table/Parts/TableHead.cs)
- [Library TableBody](/src/Dfe.Testing.Pages/Public/Components/GDS/Table/Parts/TableBody.cs)
- [Library TableRow](/src/Dfe.Testing.Pages/Public/Components/GDS/Table/Parts/TableRow.cs)
- [Library TableHeadingItem](/src/Dfe.Testing.Pages/Public/Components/GDS/Table/Parts/TableHeadingItem.cs)
- [Library TableDataItem](/src/Dfe.Testing.Pages/Public/Components/GDS/Table/Parts/TableDataItem.cs)

## Adding your own pages

When building your PageObjects you want to:

`IPageObject` - *Role:* mark pages with this

`IDocumentSessionClient` - *Role:* create pages with this

```cs
public sealed class MyPage : IPageObject

```

**Important** register your pages and application components in your DI!

```cs
    testServices...
    // top level PageObject
    testServices.AddTransient<IPageObject, HomePage>();
    testServices.AddTransient<NavigationBarComponent>(); // reusable application component

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

    // Reuse these across Pages
    public NavigationBarComponent NavBar { get; }
    public SearchComponent Search { get; }
    public FilterComponent Filter { get; }
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
        // create document
        IDocumentSessionClient documentSession = await GetTestService<IDocumentSessionClient>();
        await documentSession.RequestDocumentAsync(
            (t) => t.SetPath("/"));

        // create page from the session
        HomePage page = documentSession.GetPage<HomePage>();
    }
}

```

*Note* You could aggregate a/multiple components to your own tests type e.g `Facets` or `SearchResults` being made up of multiple GDS components to make your PageObject API more domain centric

```cs
// Basic types example
homePage.GetHeading().Should().Be("Heading"); 

public sealed class HomePage
{
    public string GetHeading() => _headingFactory.GetHeadings().Where(t => t.Type == H1).Text;
}
```

```cs
// Library types example

// GDSComponent provided by the library - record to give value-object semantics (immutable, attribute comparisons, no identity)
public record GDSTextInput
{
    public required string Name { get; init; }
    public required string Value { get; init; }
    public required string? PlaceHolder { get; init; } = null;
    public required string? Type { get; init; } = null;
}

// Page
public sealed class HomePage
{
    public HomePage(ComponentFactory<GDSTextInput> inputFactory)
    
    public GDSTextInput GetSearchInput() => inputFactory.Get(...);
}

// Test
[Fact]
public async Test()
{
    GDSTextInput expectedTextInput = new()
    {
        Name = "searchKeyWord",
        Value = "",
        PlaceHolder = "Search by keyword",
        Type = "text"
    };
    homePage.GetSearchInput().Should().Be(expectedTextInput);
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

// Any test class you inherit from BaseTest gets access to the container and a new scope is created per test

public sealed class MyTestClass : BaseTest
{
  [Fact]
  public async Task MyTest()
  {
    IDocumentSession documentSession = GetTestService<IDocumentSession>(); // is available
  }
}
```
