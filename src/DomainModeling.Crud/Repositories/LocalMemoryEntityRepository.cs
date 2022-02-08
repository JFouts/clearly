using DomainModeling.Core;
using DomainModeling.Crud;
using System.Collections.Concurrent;

namespace DomainModeling.EntityRepository;

public class LocalMemoryEntityRepository<T> : IEntityRepository<T> where T : IEntity
{
    private static readonly ConcurrentDictionary<Guid, T> _table = new ConcurrentDictionary<Guid, T>();

    public Task Delete(Guid id)
    {
        _table.TryRemove(id, out _);
        
        return Task.CompletedTask;
    }

    public Task<T> GetById(Guid id)
    {
        if (_table.TryGetValue(id, out var result))
        {
            return Task.FromResult(result);
        }

        throw new KeyNotFoundException($"Entity with id {id} was not found in table {typeof(T).Name}.");
    }

    public Task Insert(T obj)
    {
        _table.TryAdd(obj.Id, obj);

        return Task.CompletedTask;
    }

    public Task<CrudSearchResult<T>> Search(CrudSearchOptions options)
    {
        var result = new CrudSearchResult<T> { 
            Skip = options.Skip,
            Take = options.Take 
        };
        
        var query = _table.Values.AsQueryable();

        result.Count = query.Count();

        if (options.Skip > 0)
        {
            query = query.Skip(options.Skip);
        }

        if (options.Take > 0)
        {
            query = query.Take(options.Take);
        }

        result.Results = query.ToAsyncEnumerable();

        return Task.FromResult(result);
    }

    public Task Update(Guid id, T obj)
    {
        _table.AddOrUpdate(id, obj, (x,y) => obj);

        return Task.CompletedTask;
    }
}