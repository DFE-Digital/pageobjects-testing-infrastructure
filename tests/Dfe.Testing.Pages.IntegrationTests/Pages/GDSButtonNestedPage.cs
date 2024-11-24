using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dfe.Testing.Pages.Public.DocumentQueryClient.Pages;
using Dfe.Testing.Pages.Public.DocumentQueryClient.Pages.Components;
using Dfe.Testing.Pages.Public.DocumentQueryClient.Selector;

namespace Dfe.Testing.Pages.IntegrationTests.Pages;
internal sealed class GDSButtonNestedPage : IPage
{
    private readonly ComponentFactory<GDSButton> _buttonFactory;

    public GDSButtonNestedPage(ComponentFactory<GDSButton> buttonFactory)
    {
        _buttonFactory = buttonFactory;
    }

    public GDSButton GetDefaultGDSButtonNested() => _buttonFactory.Get();
    public GDSButton GetDefaultGDSButtonNestedWithScope() => _buttonFactory.Get();
    public IEnumerable<GDSButton> GetManyGDSButtonNested() => _buttonFactory.GetMany(new QueryRequestArgs()
    {
        Scope = new CssSelector("#gds-button-default-nested-nested-container")
    });

    public IEnumerable<GDSButton> GetManyDefaultGDSButtonNestedWithScope() => _buttonFactory.GetMany(new QueryRequestArgs()
    {
        Scope = new CssSelector("#gds-button-default-nested-nested-container")
    });
}
