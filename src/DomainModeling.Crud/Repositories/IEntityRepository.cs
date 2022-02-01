using DomainModeling.Core;

namespace DomainModeling.EntityRepository;

public interface IEntityRepository<T> where T : IEntity
{
    Task Delete(Guid id);
    Task<T> GetById(Guid id);
    Task Insert(T obj);
    IAsyncEnumerable<T> Search(object options);
    Task Update(Guid id, T obj);
}
