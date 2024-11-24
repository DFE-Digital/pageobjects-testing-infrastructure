﻿namespace Dfe.Testing.Pages.Internal.ComponentFactory.AnchorLink;
public sealed class AnchorLinkMapper : IComponentMapper<AnchorLinkComponent>
{
    public AnchorLinkComponent Map(IDocumentPart input)
    {
        return new()
        {
            LinkValue = input.GetAttribute("href")!,
            Text = input.Text.Trim(),
            OpensInNewTab = input.GetAttribute("target") == "_blank"
        };
    }
}
