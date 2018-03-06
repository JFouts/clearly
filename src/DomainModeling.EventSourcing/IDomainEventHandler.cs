using System.Threading.Tasks;
using DomainModeling.Core.Interfaces;

namespace DomainModeling.EventSourcing
{
    public interface IDomainEventHandler<in TAggregate, in TEvent> : IDomainEventHandler<TAggregate> where TEvent : IDomainEvent
    {
        Task HandleAsync(TAggregate aggregate, TEvent @event);
    }

    public interface IDomainEventHandler<in T>
    {
        Task HandleAsync(T aggregate, IDomainEvent @event);
    }
}
