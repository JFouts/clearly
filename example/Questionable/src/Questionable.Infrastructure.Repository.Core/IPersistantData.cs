
namespace Repository.Core;

public interface IPersistantData<out T>
{
    T Data { get; }
}
