using Dfe.Testing.Pages.Public.Components.MappingAbstraction.Request;
using Dfe.Testing.Pages.Public.Components.SelectorFactory;

namespace Dfe.Testing.Pages.Public.Components.MappingAbstraction;
public sealed class FindDocumentSectionEntryPointMapperDecorator<TComponent> : IMapper<IMapRequest<IDocumentSection>, MappedResponse<TComponent>>
    where TComponent : class
{
    private readonly IMapRequestFactory _mapRequestFactory;
    private readonly IMapper<IMapRequest<IDocumentSection>, MappedResponse<TComponent>> _decoratedMapper;
    private readonly IComponentSelectorFactory _componentSelectorFactory;
    private readonly IMappingResultFactory _mappingResultFactory;

    internal FindDocumentSectionEntryPointMapperDecorator(
        IMapRequestFactory mapRequestFactory,
        IMapper<IMapRequest<IDocumentSection>, MappedResponse<TComponent>> decoratedMapper,
        IComponentSelectorFactory componentSelectorFactory,
        IMappingResultFactory mappingResultFactory)
    {
        ArgumentNullException.ThrowIfNull(decoratedMapper);
        ArgumentNullException.ThrowIfNull(componentSelectorFactory);
        _decoratedMapper = decoratedMapper;
        _componentSelectorFactory = componentSelectorFactory;
        _mappingResultFactory = mappingResultFactory;
        _mapRequestFactory = mapRequestFactory;
    }

    public MappedResponse<TComponent> Map(IMapRequest<IDocumentSection> request)
    {
        ArgumentNullException.ThrowIfNull(request);
        ArgumentNullException.ThrowIfNull(request.From);

        // if entry point is passed, find that entrypoint from the section
        IDocumentSection? entrypointToMapFrom =
            request.EntryPoint == null
                ? request.From
                : request.From.FindDescendant(request.EntryPoint);

        if (entrypointToMapFrom == null)
        {
            return _mappingResultFactory.Create<TComponent>(
                mapped: null,
                status: MappingStatus.Failed,
                section: request.From,
                message: $"No entry point found for {typeof(TComponent).Name} using EntryPoint: {request.EntryPoint?.ToSelector() ?? string.Empty}");
        }

        return _decoratedMapper.Map(
            _mapRequestFactory.Create(entrypointToMapFrom, request.MappingResults));
    }
}
