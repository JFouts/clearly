using System;
using System.Threading.Tasks;

namespace Repositoy.Core
{
    public interface ICreate<in T>
    {
        Task CreateAsync(Guid id, T data);
    }
}