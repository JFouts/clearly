using Repository.Core;

namespace Repository.Memory;

public class MemoryPersistantData<T> : IPersistantData<T>
{
    public Guid Id { get; set; }
    public T Data { get; set; }
}
