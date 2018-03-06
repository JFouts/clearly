using System.Linq;

namespace Repositoy.Core
{
    public interface IQuery<out T>
    {
        IQueryable<T> Query { get; }
    }
}
