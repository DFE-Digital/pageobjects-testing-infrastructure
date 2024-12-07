﻿using Dfe.Testing.Pages.Public.Components.GDS.Button;
using Dfe.Testing.Pages.Public.Components.GDS.Checkbox;
using Dfe.Testing.Pages.Public.Components.GDS.Fieldset;
using Dfe.Testing.Pages.Public.Components.GDS.Inputs.TextInput;
using Dfe.Testing.Pages.Public.Components.GDS.Radio;
using Dfe.Testing.Pages.Public.Components.Inputs;
using HttpMethod = System.Net.Http.HttpMethod;

namespace Dfe.Testing.Pages.Public.Components.Form;

public record FormComponent : IComponent
{
    public required HttpMethod Method { get; init; }
    public string Action { get; init; } = string.Empty;
    public required IEnumerable<GDSFieldsetComponent> FieldSets { get; init; } = [];
    public required IEnumerable<GDSButtonComponent> Buttons { get; init; } = [];
    public IEnumerable<GDSCheckboxComponent> Checkboxes { get; init; } = [];
    public IEnumerable<GDSRadioComponent> Radios { get; init; } = [];
    public IEnumerable<GDSTextInputComponent> TextInputs { get; init; } = [];
    public IEnumerable<GDSSelectComponent> Selects { get; init; } = [];
    public IEnumerable<HiddenInputComponent> HiddenInputs { get; init; } = [];
    public bool IsFormValidated { get; init; } = true;
}