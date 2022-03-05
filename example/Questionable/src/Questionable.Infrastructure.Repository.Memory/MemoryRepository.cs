using Repository.Core;

namespace Repository.Memory;

public class MemoryRepository<T> : IRepository<T>, IQuery<T>
{
    private readonly MemoryDatabase<MemoryPersistantData<T>> _memoryDatabase;

    public IQueryable<T> Query => _memoryDatabase.Data.Select(x => x.Data).AsQueryable();

    public MemoryRepository(MemoryDatabase<MemoryPersistantData<T>> memoryDatabase)
    {
        _memoryDatabase = memoryDatabase;
    }

    public Task<IPersistantData<T>> GetByIdAsync(Guid id)
    {
        return Task.FromResult<IPersistantData<T>>(_memoryDatabase.Data.First(x => x.Id == id));
    }

    public Task CreateAsync(Guid id, T data)
    {
        _memoryDatabase.Data.Add(new MemoryPersistantData<T>
        {
            Data = data,
            Id = id
        });

        return Task.CompletedTask;
    }

    public Task UpdateAsync(Guid id, IPersistantData<T> data)
    {
        var old = _memoryDatabase.Data.First(x => x.Id == id);

        if (!(data is MemoryPersistantData<T> @new))
            throw new Exception();

        old.Data = @new.Data;

        return Task.CompletedTask;
    }
}
