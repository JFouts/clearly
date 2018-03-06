namespace Repositoy.Core
{
    public interface IRepository<T> : IGetById<T>, ICreate<T>, IUpdate<T>
    {
    }
}