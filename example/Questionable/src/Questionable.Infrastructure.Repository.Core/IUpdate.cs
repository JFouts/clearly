using System;
using System.Threading.Tasks;

namespace Repositoy.Core
{
    public interface IUpdate<in T>
    {
        Task UpdateAsync(Guid id, IPersistantData<T> data);
    }
}