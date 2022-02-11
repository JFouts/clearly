using DomainModeling.Crud.WebUi.ViewModels;

namespace DomainModeling.Crud.WebUi.Factories;

public interface IListViewModelFactory<TEntity>
{
    Task<ListViewModel> Build(CrudSearchResult<TEntity> result, int page, int pageSize);
}
