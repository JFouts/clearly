
namespace Repository.Core;

public interface IQuery<out T>
{
    IQueryable<T> Query { get; }
}
