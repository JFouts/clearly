using System;
using System.Threading.Tasks;

namespace Repository.Core
{
    public interface IGetById<T>
    {
        Task<IPersistantData<T>> GetByIdAsync(Guid id);
    }
}