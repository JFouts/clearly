namespace DomainModeling.Crud;

public interface ICrudService<T> : IService
{
    IAsyncEnumerable<T> Search(CrudSearchOptions options);
    Task<T> GetById(string id);
    Task Update(string id, T obj);
    Task Insert(T obj);
    Task Delete(string id);
}
