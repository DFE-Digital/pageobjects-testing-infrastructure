﻿using Dfe.Testing.Pages.Components.Checkbox;
using Dfe.Testing.Pages.Public.Mapper.Interface;

namespace Dfe.Testing.Pages.Public.Mapper;
internal sealed class GDSCheckboxMapper : IComponentMapper<GDSCheckboxComponent>
{
    internal static IElementSelector GDSCheckboxInputStyle => new CssSelector(".govuk-checkboxes__input");
    internal static IElementSelector GDSLabelStyle => new CssSelector(".govuk-checkboxes__label");

    public GDSCheckboxComponent Map(IDocumentPart input)
    {
        var checkboxItem = input.GetChild(GDSCheckboxInputStyle) ?? throw new ArgumentNullException(nameof(GDSCheckboxInputStyle));
        var checkboxLabel = input.GetChild(GDSLabelStyle) ?? throw new ArgumentNullException(nameof(GDSLabelStyle));

        return new GDSCheckboxComponent()
        {
            TagName = "",
            Name = checkboxItem.GetAttribute("name") ?? throw new ArgumentNullException($"no name of {nameof(checkboxItem)}")!,
            Label = checkboxLabel.Text ?? throw new ArgumentNullException($"no label on {nameof(checkboxItem)}"),
            Value = checkboxItem.GetAttribute("value") ?? throw new ArgumentNullException($"no value on {nameof(checkboxItem)}"),
            Checked = checkboxItem.HasAttribute("checked")
        };
    }
}