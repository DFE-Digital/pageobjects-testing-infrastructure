using Dfe.Testing.Pages.Public.Components.Link;
using Dfe.Testing.Pages.Public.Components.MappingAbstraction;
using Dfe.Testing.Pages.Public.Components.MappingAbstraction.Request;
using Dfe.Testing.Pages.Public.Components.Text;

namespace Dfe.Testing.Pages.Public.Components.GDS.Button;
internal class GDSButtonMapper : IMapper<IMapRequest<IDocumentSection>, MappedResponse<GDSButtonComponent>>
{
    private readonly IMapRequestFactory _mapRequestFactory;
    private readonly IMappingResultFactory _mappingResultFactory;
    private readonly IMapper<IMapRequest<IDocumentSection>, MappedResponse<TextComponent>> _textMapper;
    private readonly IGDSButtonBuilder _buttonBuilder;

    public GDSButtonMapper(
        IMapRequestFactory mapRequestFactory,
        IMappingResultFactory mappingResultFactory,
        IMapper<IMapRequest<IDocumentSection>, MappedResponse<TextComponent>> textMapper,
        IGDSButtonBuilder buttonBuilder)
    {
        ArgumentNullException.ThrowIfNull(textMapper);
        ArgumentNullException.ThrowIfNull(buttonBuilder);
        _mapRequestFactory = mapRequestFactory;
        _mappingResultFactory = mappingResultFactory;
        _textMapper = textMapper;
        _buttonBuilder = buttonBuilder;
    }

    public MappedResponse<GDSButtonComponent> Map(IMapRequest<IDocumentSection> request)
    {
        MappedResponse<TextComponent> text = _textMapper.Map(
            _mapRequestFactory.Create(
                request.From,
                request.MappingResults));

        request.MappingResults.Add(text.MappingResult);

        string buttonStyles = request.From.GetAttribute("class") ?? string.Empty;

        ButtonStyleType buttonType =
                buttonStyles.Contains("govuk-button--secondary") ? ButtonStyleType.Secondary :
                buttonStyles.Contains("govuk-button--warning") ? ButtonStyleType.Warning
                    : ButtonStyleType.Primary;

        _buttonBuilder.SetValue(request.From.GetAttribute("value") ?? string.Empty)
            .SetName(request.From.GetAttribute("name") ?? string.Empty)
            .SetText(text.Mapped?.Text ?? string.Empty)
            .SetEnabled(!request.From.HasAttribute("disabled"))
            .SetButtonStyle(buttonType)
            .SetType(request.From.GetAttribute("type") ?? string.Empty)
            .Build();

        return _mappingResultFactory.Create(
                mapped: _buttonBuilder.Build(),
                status: MappingStatus.Success,
                section: request.From);
    }
}
