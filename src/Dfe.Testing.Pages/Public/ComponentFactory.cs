﻿using Dfe.Testing.Pages.Components;
using Dfe.Testing.Pages.Public.Mapper.Abstraction;

namespace Dfe.Testing.Pages.Public;
public class ComponentFactory<T> where T : IComponent
{
    private readonly IComponentDefaultSelectorFactory _componentSelectorFactory;
    private readonly IDocumentQueryClientAccessor _documentQueryClientAccessor;
    private readonly IComponentMapper<T> _mapper;

    public ComponentFactory(
        IComponentDefaultSelectorFactory componentSelectorFactory,
        IDocumentQueryClientAccessor documentQueryClientAccessor,
        IComponentMapper<T> mapper)
    {
        ArgumentNullException.ThrowIfNull(documentQueryClientAccessor);
        _componentSelectorFactory = componentSelectorFactory;
        _documentQueryClientAccessor = documentQueryClientAccessor;
        _mapper = mapper;
    }

    internal IDocumentQueryClient DocumentQueryClient => _documentQueryClientAccessor.DocumentQueryClient;
    internal virtual QueryRequestArgs MergeRequest(QueryRequestArgs? request, IElementSelector defaultFindBySelector)
    {
        ArgumentNullException.ThrowIfNull(defaultFindBySelector);
        return new()
        {
            Query = request?.Query ?? defaultFindBySelector,
            Scope = request?.Scope
        };
    }

    public virtual T Get(QueryRequestArgs? request = null) => GetMany(request).Single();

    public virtual IList<T> GetMany(QueryRequestArgs? request = null)
    {
        return DocumentQueryClient.QueryMany(
            MergeRequest(
                request, _componentSelectorFactory.GetSelector<T>()),
            mapper: _mapper.Map).ToList();
    }

    internal virtual T GetFromPart(IDocumentPart? part) => _mapper.Map(part ?? throw new ArgumentNullException(nameof(part)));

    internal virtual IList<T> GetManyFromPart(IDocumentPart? part)
        => part?
            .GetChildren(_componentSelectorFactory.GetSelector<T>())?
            .Select(_mapper.Map)
            .ToList() ?? throw new ArgumentNullException(nameof(part));

}
