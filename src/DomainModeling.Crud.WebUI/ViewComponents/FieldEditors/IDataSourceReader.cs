using DomainModeling.Crud.Services;

namespace DomainModeling.Crud.WebUi.ViewComponents.FieldEditors;

public interface IDataSourceReader<TModel>
{
    Task<IEnumerable<TModel>> ReadFrom(IDataSource dataSource);
}
