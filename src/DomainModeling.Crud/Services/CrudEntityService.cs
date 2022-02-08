using DomainModeling.Core;
using DomainModeling.EntityRepository;

namespace DomainModeling.Crud.Services;

internal class CrudEntityService<T> : ICrudService<T> where T : IEntity
{
    private readonly IEntityRepository<T> _repository;

    public CrudEntityService(IEntityRepository<T> repository)
    {
        _repository = repository;
    }

    public async Task Delete(Guid id)
    {
        await _repository.Delete(id);
    }

    public async Task<T> GetById(Guid id)
    {
        return await _repository.GetById(id);
    }

    public async Task Insert(T obj)
    {
        if (obj.Id == Guid.Empty) {
            obj.Id = Guid.NewGuid();
        }

        await _repository.Insert(obj);
    }

    public async Task<CrudSearchResult<T>> Search(CrudSearchOptions options)
    {
        return await _repository.Search(options);
    }

    public async Task Update(Guid id, T obj)
    {
        await _repository.Update(id, obj);
    }
}