using System.Reflection;
using DomainModeling.Core;
using DomainModeling.Crud.WebUi.Extensions;
using DomainModeling.Crud.WebUi.Models;
using DomainModeling.Crud.WebUi.Utilities;
using DomainModeling.Crud.WebUi.ViewModels;

namespace DomainModeling.Crud.WebUi.Factories;

public class ListViewModelFactory<TEntity> : IListViewModelFactory<TEntity> where TEntity : IEntity
{
    private readonly IFieldDefinitionFactory _fieldDefintionFactory;

    public ListViewModelFactory(IFieldDefinitionFactory fieldDefintionFactory)
    {
        _fieldDefintionFactory = fieldDefintionFactory;
    }

    public async Task<ListViewModel> Build(CrudSearchResult<TEntity> result, int page, int pageSize)
    {
        var entity = typeof(TEntity);
        
        return new ListViewModel {
            DisplayName = entity.Name.SplitCamelCase(),
            Columns = CreateListViewColumnFromType(entity),
            PageCount = ((result.Count - 1) / pageSize) + 1,
            CurrentPage = page,
            Results = await result.Results.Select(e => e.ToDictionary()).ToListAsync()
        };
    }

    private IEnumerable<TableColumnViewModel> CreateListViewColumnFromType(Type entity)
    {
        var columns = entity
            .GetProperties()
            .Where(x => x.Name != "Id")
            .Select(CreateTableColumnViewModel)
            .ToList();

        columns.Add(new TableColumnViewModel {
            DisplayName = "Actions",
            Key = "id",
            FieldDefintion = new FieldDefintion {
                FieldDisplayType = "TableActions",
                FieldName = "Actions",
                Properties = new Dictionary<string, object> {
                    { "EditEnabled", true },
                    { "DeleteEnabled", true },
                }
            }
        });

        return columns;
    }
    
    private TableColumnViewModel CreateTableColumnViewModel(PropertyInfo property)
    {
        var defintion = new TableColumnViewModel
        {
            DisplayName = property.Name.SplitCamelCase(),
            Key = property.Name.LowerCamelCase(),
            FieldDefintion = _fieldDefintionFactory.CreateFieldDefinition(property)
        };

        return defintion;
    }
    

}
