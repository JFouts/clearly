using System;
using System.Threading.Tasks;

namespace Repositoy.Core
{
    public interface IGetById<T>
    {
        Task<IPersistantData<T>> GetByIdAsync(Guid id);
    }
}