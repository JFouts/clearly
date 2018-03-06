using System.Threading.Tasks;
using DomainModeling.Core.Interfaces;

namespace DomainModeling.EventSourcing
{
    public interface IEventDispatcher<in T>
    {
        Task DispatchAsync(T aggregate, IDomainEvent @event);
    }
}
