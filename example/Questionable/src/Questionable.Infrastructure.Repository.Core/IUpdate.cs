
namespace Repository.Core;

public interface IUpdate<in T>
{
    Task UpdateAsync(Guid id, IPersistantData<T> data);
}
