using Dfe.Testing.Pages.Public.Components.GDS.ErrorMessage;
using Dfe.Testing.Pages.Public.Components.Label;

namespace Dfe.Testing.Pages.Public.Components.GDS.Select;
internal sealed class GDSSelectMapper : IComponentMapper<GDSSelectComponent>
{
    private readonly IMapRequestFactory _mapRequestFactory;
    private readonly IMappingResultFactory _mappingResultFactory;
    private readonly IComponentMapper<LabelComponent> _labelMapper;
    private readonly IComponentMapper<OptionComponent> _optionMapper;
    private readonly IComponentMapper<GDSErrorMessageComponent> _errorMessageMapper;

    public GDSSelectMapper(
        IMappingResultFactory mappingResultFactory,
        IComponentMapper<LabelComponent> labelMapper,
        IComponentMapper<OptionComponent> optionMapper,
        IComponentMapper<GDSErrorMessageComponent> errorMessageMapper,
        IMapRequestFactory mapRequestFactory)
    {
        _labelMapper = labelMapper;
        _optionMapper = optionMapper;
        _errorMessageMapper = errorMessageMapper;
        _mappingResultFactory = mappingResultFactory;
        _mapRequestFactory = mapRequestFactory;
    }

    public MappedResponse<GDSSelectComponent> Map(IMapRequest<IDocumentSection> request)
    {
        MappedResponse<LabelComponent> mappedLabel =
            _labelMapper.Map(
                _mapRequestFactory.CreateRequestFrom(request, nameof(GDSSelectComponent.Label)))
            .AddToMappingResults(request.MappedResults);

        IEnumerable<MappedResponse<OptionComponent>> mappedOptions =
            _mapRequestFactory.CreateRequestFrom(request, nameof(GDSSelectComponent.Options))
                .FindManyDescendantsAndMapToComponent(_mapRequestFactory, _optionMapper)
                .AddToMappingResults(request.MappedResults);

        GDSSelectComponent select = new()
        {
            Label = mappedLabel.Mapped,
            Options = mappedOptions.Select(t => t.Mapped!),
            ErrorMessage = _errorMessageMapper.Map(request).Mapped!
        };

        return _mappingResultFactory.Create(
            select,
            MappingStatus.Success,
            request.Document);
    }
}
