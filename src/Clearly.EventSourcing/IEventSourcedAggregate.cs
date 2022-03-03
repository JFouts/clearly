using System.Collections.Generic;
using System.Threading.Tasks;
using Clearly.Core;
using Clearly.Core.Interfaces;

namespace Clearly.EventSourcing
{
    public interface IEventSourcedAggregate<out T> : IAggregate<T> where T : AggregateRoot
    {
        IEnumerable<IDomainEvent> UnrecordedEvents { get; }
        Task FireEventAsync(IDomainEvent @event);
        Task DispatchEventAsync(IDomainEvent @event);
    }
}