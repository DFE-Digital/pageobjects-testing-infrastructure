using Dfe.Testing.Pages.Public.Components.GDS.ErrorMessage;
using Dfe.Testing.Pages.Public.Components.Label;
using Dfe.Testing.Pages.Public.Components.MappingAbstraction.Request;
using Dfe.Testing.Pages.Public.Components.SelectorFactory;

namespace Dfe.Testing.Pages.Public.Components.GDS.Select;
internal sealed class GDSSelectMapper : IMapper<IMapRequest<IDocumentSection>, MappedResponse<GDSSelectComponent>>
{
    private readonly IMapRequestFactory _mapRequestFactory;
    private readonly IMappingResultFactory _mappingResultFactory;
    private readonly IComponentSelectorFactory _componentSelectorFactory;
    private readonly IMapper<IMapRequest<IDocumentSection>, MappedResponse<LabelComponent>> _labelMapper;
    private readonly IMapper<IMapRequest<IDocumentSection>, MappedResponse<OptionComponent>> _optionMapper;
    private readonly IMapper<IMapRequest<IDocumentSection>, MappedResponse<GDSErrorMessageComponent>> _errorMessageMapper;

    public GDSSelectMapper(
        IMapRequestFactory mapRequestFactory,
        IMappingResultFactory mappingResultFactory,
        IComponentSelectorFactory componentSelectorFactory,
        IMapper<IMapRequest<IDocumentSection>, MappedResponse<LabelComponent>> labelMapper,
        IMapper<IMapRequest<IDocumentSection>, MappedResponse<OptionComponent>> optionMapper,
        IMapper<IMapRequest<IDocumentSection>, MappedResponse<GDSErrorMessageComponent>> errorMessageMapper)
    {
        _componentSelectorFactory = componentSelectorFactory;
        _labelMapper = labelMapper;
        _optionMapper = optionMapper;
        _errorMessageMapper = errorMessageMapper;
        _mappingResultFactory = mappingResultFactory;
        _mapRequestFactory = mapRequestFactory;
    }

    public MappedResponse<GDSSelectComponent> Map(IMapRequest<IDocumentSection> request)
    {
        MappedResponse<LabelComponent> mappedLabel = _labelMapper.Map(request).AddMappedResponseToResults(request.MappingResults);

        IEnumerable<MappedResponse<OptionComponent>> mappedOptions = request.FindManyDescendantsAndMap(
            _mapRequestFactory,
            _componentSelectorFactory.GetSelector<OptionComponent>(),
            _optionMapper)
        .AddMappedResponseToResults(request.MappingResults);

        GDSSelectComponent select = new()
        {
            Label = mappedLabel.Mapped,
            Options = mappedOptions.Select(t => t.Mapped!),
            ErrorMessage = _errorMessageMapper.Map(request).Mapped!
        };

        return _mappingResultFactory.Create(
            select,
            MappingStatus.Success,
            request.From);
    }
}
