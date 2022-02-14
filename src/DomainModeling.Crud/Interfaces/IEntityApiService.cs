using DomainModeling.Core;

namespace DomainModeling.Crud.Services;

public interface IEntityApiService<TEntity> where TEntity : IEntity
{
    Task<CrudSearchResult<TEntity>> Search();
    Task<TEntity> GetById(Guid id);
    Task Create(TEntity value);
    Task Update(Guid id, TEntity value);
    Task Delete(Guid id);
}
