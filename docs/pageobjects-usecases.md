# PageObject Use cases

Application PageObject - a representation of an application page.

Application Component - an application component that appears in different pages of the app. These should not be duplicated every time they are needed.

## Creating an application PageObject

```cs
// mark as a pageobject
public sealed class MyPage : IPageObject
{
  
}

// register in DI
services.AddTransient<IPageObject, MyPage>();

// use in test
[Fact]
public void MyTest()
{
    MyPage page = ResolveFromDI<MyPage>();
}

```

## Creating a reusable application component

```cs
public sealed class MyReuseablePageComponent
{
    private readonly ComponentFactory<TextComponent> _textFactory;
    private readonly ComponentFactory<GDSButtonComponent> _buttonFactory;
    
    public MyReuseablePageComponent(
        ComponentFactory<TextComponent> textFactory,
        ComponentFactory<GDSButtonComponent> buttonFactory)
        {
            _textFactory = textFactory;
            _buttonFactory = buttonFactory;
        }
    
    public GDSButtonComponent GetButton(string scope) => _buttonFactory.Create(new(){
        InScope = scope;
    })
}

// use in a pageobject by adding to constructor
public sealed class MyPage : IPageObject
{
    public MyPage(MyReuseableComponent component)
    {
        Component = component;
    }

    MyReuseableComponent Component { get; }
}

// register both types so DI can resolve
services.AddTransient<IPageObject, MyPage>()
services.AddTransient<MyReuseablePageComponent>();

// use in test
[Fact]
public void MyTest()
{
    MyPage page = ResolveFromDI<MyPage>();
    page.Component.GetButton()....
}

[Fact]
public void AnotherPageTest()
{
    AnotherPage page = ResolveFromDI<AnotherPage>();
    page.Component.GetButton(); // OR wrap in a method at pageobject and pass another scope / locator to the component call when page.GetButton() is called
}
```

## Create an application PageObject that uses GDSComponent(s)

```cs
// mark as a pageobject
// take in the ComponentFactory<TComponent> into the constructor of the page
// expose method which calls library
public sealed class MyPage : IPageObject
{
    private readonly ComponentFactory<AnchorLinkComponent> _linkFactory;

    public MyPage(ComponentFactory<AnchorLinkComponent> linkFactory)
    {
        _linkFactory = linkFactory;
    }

    public AnchorLinkComponent GetHomeLink()
    {
        CreatedComponentResponse response = _linkFactory.Create
        (
            InScope = new CssElementSelector("#nav-bar");
            // options...
        )
        return response.Created;
    }
}


// register in DI
services.AddTransient<IPageObject, MyPage>();

// use in test
[Fact]
public void MyTest()
{
    MyPage page = ResolveFromDI<MyPage>();
    AnchorLinkComponent component = ResolveFromDI<IAnchorLinkComponentBuilder>().SetLink("/").Build();

    page.GetHomeLink.Should().BeEquivalentTo(component);
}

```