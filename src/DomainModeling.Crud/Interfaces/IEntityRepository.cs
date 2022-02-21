using DomainModeling.Core;
using DomainModeling.Crud.Search;

namespace DomainModeling.Crud;

public interface IEntityRepository<T> where T : IEntity
{
    Task Delete(Guid id);
    Task<T> GetById(Guid id);
    Task Insert(T obj);
    Task<CrudSearchResult<T>> Search(CrudSearchOptions options);
    Task Update(Guid id, T obj);
}
