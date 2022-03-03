// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using DomainModeling.Core;
using DomainModeling.Crud.Search;
using DomainModeling.Crud.WebUi.Extensions;
using DomainModeling.Crud.WebUi.ViewModels;

namespace DomainModeling.Crud.WebUi.Factories;

/// <summary>
/// Creates the View Model for the Search page for an Entity
/// </summary>
public class SearchListViewModelFactory<TEntity> : ISearchListViewModelFactory<TEntity>
    where TEntity : IEntity
{
    private readonly IEntityDefinitionFactory entityDefinitionFactory;

    public SearchListViewModelFactory(IEntityDefinitionFactory entityDefinitionFactory)
    {
        this.entityDefinitionFactory = entityDefinitionFactory;
    }

    public async Task<ListViewModel> Build(CrudSearchResult<TEntity> result, int page, int pageSize)
    {
        var definition = entityDefinitionFactory.CreateFor<TEntity>();

        return new ListViewModel
        {
            DisplayName = definition.DisplayName,
            Columns = CreateListViewColumnFromType(definition),
            PageCount = ((result.Count - 1) / pageSize) + 1,
            CurrentPage = page,
            Results = await result.Results.Select(e => e.ToDictionary()).ToListAsync(),
        };
    }

    private IEnumerable<TableColumnViewModel> CreateListViewColumnFromType(EntityDefinition entity)
    {
        var columns = entity
            .Fields
            .Where(x => x.Using<CrudAdminEntityFieldFeature>().DisplayOnSearch)
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

    private TableColumnViewModel BuildColumnViewModel(EntityFieldDefinition field)
    {
        var metadata = field.Using<CrudAdminEntityFieldFeature>();

        return new TableColumnViewModel
        {
            DisplayName = field.DisplayName,
            Key = field.Property.Name,
            DisplayTemplate = metadata.DisplayTemplate,
            Properties = metadata.DisplayProperties,
        };
    }
}
