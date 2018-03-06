namespace Repositoy.Core
{
    public interface IPersistantData<out T>
    {
        T Data { get; }
    }
}