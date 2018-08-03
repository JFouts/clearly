using System.Threading.Tasks;
using DomainModeling.Core.DomainObjectTypes;

namespace DomainModeling.EventSourcing
{
    public interface IEventDispatcher<in T>
    {
        Task DispatchAsync(T aggregate, DomainEvent @event);
    }
}
