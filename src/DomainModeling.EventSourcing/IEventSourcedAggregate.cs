using System.Collections.Generic;
using System.Threading.Tasks;
using DomainModeling.Core;
using DomainModeling.Core.Interfaces;

namespace DomainModeling.EventSourcing
{
    public interface IEventSourcedAggregate<out T> : IAggregate<T> where T : AggregateRoot
    {
        IEnumerable<IDomainEvent> UnrecordedEvents { get; }
        Task FireEventAsync(IDomainEvent @event);
        Task DispatchEventAsync(IDomainEvent @event);
    }
}