using System;
using System.Threading.Tasks;

namespace Repository.Core
{
    public interface ICreate<in T>
    {
        Task CreateAsync(Guid id, T data);
    }
}