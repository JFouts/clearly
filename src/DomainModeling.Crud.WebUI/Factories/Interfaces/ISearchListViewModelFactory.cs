using DomainModeling.Crud.Search;
using DomainModeling.Crud.WebUi.ViewModels;

namespace DomainModeling.Crud.WebUi.Factories;

public interface ISearchListViewModelFactory<TEntity>
{
    Task<ListViewModel> Build(CrudSearchResult<TEntity> result, int page, int pageSize);
}
