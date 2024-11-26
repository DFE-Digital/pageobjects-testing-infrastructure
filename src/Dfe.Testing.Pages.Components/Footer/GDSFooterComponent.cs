﻿using Dfe.Testing.Pages.Components.AnchorLink;

namespace Dfe.Testing.Pages.Components.Footer;
public record GDSFooterComponent : IComponent
{
    public required string LicenseMessage { get; init; }
    public IEnumerable<AnchorLinkComponent> ApplicationLinks { get; init; } = [];
    public required AnchorLinkComponent LicenseLink { get; init; }
    public required AnchorLinkComponent CrownCopyrightLink { get; init; }
}
