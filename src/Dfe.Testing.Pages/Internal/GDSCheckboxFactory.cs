﻿using Dfe.Testing.Pages.Components.Checkbox;
using Dfe.Testing.Pages.Internal.Mapper;

namespace Dfe.Testing.Pages.Internal;

internal sealed class GDSCheckboxFactory : ComponentFactory<GDSCheckboxComponent>
{
    private readonly IComponentMapper<GDSCheckboxComponent> _mapper;

    internal static IElementSelector GDSCheckboxItemStyle => new CssSelector(".govuk-checkboxes__item");


    public GDSCheckboxFactory(
        IComponentSelectorFactory componentSelectorFactory,
        IDocumentQueryClientAccessor documentQueryClientAccessor,
        IComponentMapper<GDSCheckboxComponent> mapper
        ) : base(componentSelectorFactory, documentQueryClientAccessor, mapper)
    {
        ArgumentNullException.ThrowIfNull(mapper);
        _mapper = mapper;
    }

    // TODO Checkbox component
    // TODO label component

    internal List<GDSCheckboxComponent> GetCheckboxesFromPart(IDocumentPart? part)
        => part?
            .GetChildren(GDSCheckboxItemStyle)?
            .Select(_mapper.Map)
            .ToList() ?? throw new ArgumentNullException(nameof(part));
}
