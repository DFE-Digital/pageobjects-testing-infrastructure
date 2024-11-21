namespace Dfe.Testing.Pages.Internal.WebDriver;
internal sealed class ApplicationNavigatorAccessor : IApplicationNavigatorAccessor
{
    private IApplicationNavigator? _navigator = null!;
    public IApplicationNavigator Navigator
    {
        get => _navigator ?? throw new ArgumentException("ApplicationNavigator has not been set", nameof(_navigator));
        set => _navigator = value;
    }
}
