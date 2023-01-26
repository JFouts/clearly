// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Clearly.Core;
using Clearly.Crud.Models.EntityGraph;
using Clearly.Crud.Search;
using Clearly.Crud.WebUi.Extensions;
using Clearly.Crud.WebUi.ViewModels;

namespace Clearly.Crud.WebUi.Factories;

/// <inheritdoc/>
public class SearchListViewModelFactory<TEntity> : ISearchListViewModelFactory<TEntity>
    where TEntity : IEntity
{
    private readonly IEntityDefinitionGraphFactory entityDefinitionFactory;

    /// <summary>
    /// Initializes a new instance of the <see cref="SearchListViewModelFactory{TEntity}"/> class.
    /// </summary>
    /// <param name="entityDefinitionFactory">The factory used to create entity definitions.</param>
    public SearchListViewModelFactory(IEntityDefinitionGraphFactory entityDefinitionFactory)
    {
        this.entityDefinitionFactory = entityDefinitionFactory;
    }

    /// <inheritdoc/>
    public Task<ListViewModel> Build(CrudSearchResult<TEntity> result, int page, int pageSize)
    {
        var definition = entityDefinitionFactory.CreateForEntity<TEntity>();

        return Task.FromResult(new ListViewModel
        {
            DisplayName = definition.DisplayName,
            Columns = CreateListViewColumnFromType(definition),
            PageCount = ((result.Count - 1) / pageSize) + 1,
            CurrentPage = page,
            Results = result.Results.Select(e => e.ToDictionary()).ToList(),
        });
    }

    private IEnumerable<TableColumnViewModel> CreateListViewColumnFromType(EntityTypeDefinitionNode entity)
    {
        var columns = entity
            .Properties
            .Where(x => x.Using<CrudAdminPropertyFeature>().DisplayOnSearch)
            .Select(BuildColumnViewModel)
            .ToList();

        columns.Add(new TableColumnViewModel
        {
            // TODO: Hard coding this is hacky
            DisplayName = "Actions",
            Key = nameof(IEntity.Id),
            DisplayTemplate = "TableActions",
            Properties = new Dictionary<string, object>
            {
                { "EditEnabled", true },
                { "DeleteEnabled", true },
            },
        });

        return columns;
    }

    private TableColumnViewModel BuildColumnViewModel(PropertyDefinitionNode property)
    {
        var metadata = property.Using<CrudAdminPropertyFeature>();

        return new TableColumnViewModel
        {
            DisplayName = property.DisplayName,
            Key = property.Property.Name,
            DisplayTemplate = metadata.DisplayComponentName,
            //Properties = metadata.DisplayProperties,
        };
    }
}
