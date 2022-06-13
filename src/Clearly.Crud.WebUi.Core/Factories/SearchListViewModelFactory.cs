// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Clearly.Core;
using Clearly.Crud.Search;
using Clearly.Crud.WebUi.Extensions;
using Clearly.Crud.WebUi.ViewModels;

namespace Clearly.Crud.WebUi.Factories;

/// <inheritdoc/>
public class SearchListViewModelFactory<TEntity> : ISearchListViewModelFactory<TEntity>
    where TEntity : IEntity
{
    private readonly IEntityDefinitionFactory entityDefinitionFactory;

    /// <summary>
    /// Initializes a new instance of the <see cref="SearchListViewModelFactory{TEntity}"/> class.
    /// </summary>
    /// <param name="entityDefinitionFactory">The factory used to create entity definitons.</param>
    public SearchListViewModelFactory(IEntityDefinitionFactory entityDefinitionFactory)
    {
        this.entityDefinitionFactory = entityDefinitionFactory;
    }

    /// <inheritdoc/>
    public async Task<ListViewModel> Build(CrudSearchResult<TEntity> result, int page, int pageSize)
    {
        var definition = entityDefinitionFactory.CreateForEntity<TEntity>();

        return new ListViewModel
        {
            DisplayName = definition.DisplayName,
            Columns = CreateListViewColumnFromType(definition),
            PageCount = ((result.Count - 1) / pageSize) + 1,
            CurrentPage = page,
            //TODO: Results = await result.Results.Select(e => e.ToDictionary()).ToListAsync(),
            Results = result.Results.Select(e => e.ToDictionary()).ToList(),
        };
    }

    private IEnumerable<TableColumnViewModel> CreateListViewColumnFromType(EntityDefinition entity)
    {
        var columns = entity
            .Fields
            .Where(x => x.Using<CrudAdminFieldFeature>().DisplayOnSearch)
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

    private TableColumnViewModel BuildColumnViewModel(FieldDefinition field)
    {
        var metadata = field.Using<CrudAdminFieldFeature>();

        return new TableColumnViewModel
        {
            DisplayName = field.DisplayName,
            Key = field.Property.Name,
            DisplayTemplate = metadata.DisplayComponentName,
            //Properties = metadata.DisplayProperties,
        };
    }
}
