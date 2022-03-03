using System.Threading.Tasks;
using Clearly.Core.Interfaces;

namespace Clearly.EventSourcing
{
    public interface IEventDispatcher<in T>
    {
        Task DispatchAsync(T aggregate, IDomainEvent @event);
    }
}
