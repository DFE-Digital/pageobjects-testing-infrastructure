using Dfe.Testing.Pages.Public.Components.Text;

namespace Dfe.Testing.Pages.Public.Components.GDS.Button;
internal class GDSButtonMapper : IComponentMapper<GDSButtonComponent>
{
    private readonly IMappingResultFactory _mappingResultFactory;
    private readonly IMapRequestFactory _mapRequestFactory;
    private readonly IComponentMapper<TextComponent> _textMapper;
    private readonly IGDSButtonBuilder _buttonBuilder;

    public GDSButtonMapper(
        IMapRequestFactory mapRequestFactory,
        IComponentMapper<TextComponent> textMapper,
        IGDSButtonBuilder buttonBuilder,
        IMappingResultFactory mappingResultFactory)
    {
        ArgumentNullException.ThrowIfNull(textMapper);
        ArgumentNullException.ThrowIfNull(buttonBuilder);
        _mappingResultFactory = mappingResultFactory;
        _mapRequestFactory = mapRequestFactory;
        _textMapper = textMapper;
        _buttonBuilder = buttonBuilder;
    }

    public MappedResponse<GDSButtonComponent> Map(IMapRequest<IDocumentSection> request)
    {
        MappedResponse<TextComponent> text =
            _textMapper.Map(
                _mapRequestFactory.CreateRequestFrom(request, nameof(GDSButtonComponent.Text)))
            .AddToMappingResults(request.MappedResults);

        string buttonStyles = request.Document.GetAttribute("class") ?? string.Empty;

        ButtonStyleType buttonType =
                buttonStyles.Contains("govuk-button--secondary") ? ButtonStyleType.Secondary :
                buttonStyles.Contains("govuk-button--warning") ? ButtonStyleType.Warning
                    : ButtonStyleType.Primary;

        _buttonBuilder.SetValue(request.Document.GetAttribute("value") ?? string.Empty)
            .SetName(request.Document.GetAttribute("name") ?? string.Empty)
            .SetText(text.Mapped?.Text ?? string.Empty)
            .SetEnabled(!request.Document.HasAttribute("disabled"))
            .SetButtonStyle(buttonType)
            .SetType(request.Document.GetAttribute("type") ?? string.Empty)
            .Build();

        return _mappingResultFactory.Create(
                mapped: _buttonBuilder.Build(),
                status: MappingStatus.Success,
                section: request.Document);
    }
}
