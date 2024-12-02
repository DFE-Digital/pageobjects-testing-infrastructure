﻿using Dfe.Testing.Pages.Components.ErrorMessage;
using Dfe.Testing.Pages.Components.Label;
using Dfe.Testing.Pages.Components.Select;
using Dfe.Testing.Pages.Public.Mapper.Abstraction;

namespace Dfe.Testing.Pages.Public.Mapper.GDS;
internal sealed class GDSSelectMapper : IComponentMapper<GDSSelectComponent>
{
    private readonly ComponentFactory<LabelComponent> _labelFactory;
    private readonly ComponentFactory<OptionComponent> _optionFactory;
    private readonly ComponentFactory<GDSErrorMessageComponent> _errorMessageFactory;

    public GDSSelectMapper(
        ComponentFactory<LabelComponent> labelFactory,
        ComponentFactory<OptionComponent> optionFactory,
        ComponentFactory<GDSErrorMessageComponent> errorMessageFactory)
    {
        _labelFactory = labelFactory;
        _optionFactory = optionFactory;
        _errorMessageFactory = errorMessageFactory;
    }
    public GDSSelectComponent Map(IDocumentPart input)
    {
        return new GDSSelectComponent()
        {
            Label = _labelFactory.GetManyFromPart(input).Single(),
            Options = _optionFactory.GetManyFromPart(input),
            ErrorMessage = _errorMessageFactory.GetManyFromPart(input).SingleOrDefault()
                ?? new GDSErrorMessageComponent() { ErrorMessage = string.Empty }
        };
    }
}
