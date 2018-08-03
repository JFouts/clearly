using System.Threading.Tasks;
using DomainModeling.Core.DomainObjectTypes;

namespace DomainModeling.EventSourcing
{
    public interface IDomainEventHandler<in TAggregate, in TEvent> : IDomainEventHandler<TAggregate> where TEvent : DomainEvent
    {
        Task HandleAsync(TAggregate aggregate, TEvent @event);
    }

    public interface IDomainEventHandler<in T>
    {
        Task HandleAsync(T aggregate, DomainEvent @event);
    }
}
