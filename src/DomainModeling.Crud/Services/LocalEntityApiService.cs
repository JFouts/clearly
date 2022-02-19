using DomainModeling.Core;

namespace DomainModeling.Crud.Services;

// TODO: Well defined exceptions that come from this class

public class LocalEntityApiService<TEntity> : IEntityApiService<TEntity> where TEntity : IEntity
{
    private readonly ICrudService<TEntity> _service;

    public LocalEntityApiService(ICrudService<TEntity> service)
    {
        _service = service;
    }

    public async Task<CrudSearchResult<TEntity>> Search(CrudSearchOptions searchOptions)
    {
        return await _service.Search(searchOptions);
    }

    public async Task<TEntity> GetById(Guid id)
    {
        return await _service.GetById(id);
    }

    public async Task Create(TEntity value)
    {
        await _service.Insert(value);
    }

    public async Task Update(Guid id, TEntity value)
    {
        await _service.Update(id, value);
    }

    public async Task Delete(Guid id)
    {
        await _service.Delete(id);
    }
}
