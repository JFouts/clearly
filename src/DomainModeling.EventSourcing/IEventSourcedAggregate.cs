using System.Collections.Generic;
using System.Threading.Tasks;
using DomainModeling.Core;
using DomainModeling.Core.DomainObjectTypes;

namespace DomainModeling.EventSourcing
{
    public interface IEventSourcedAggregate<out T> : IAggregate<T> where T : AggregateRoot
    {
        IEnumerable<DomainEvent> UnrecordedEvents { get; }
        Task FireEventAsync(DomainEvent @event);
        Task DispatchEventAsync(DomainEvent @event);
    }
}