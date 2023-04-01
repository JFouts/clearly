using Clearly.Core.Interfaces;

namespace Clearly.EventRepository
{
    public interface IEventRepository
    {
        Task SaveNewStreamEventsAsync<T>(Guid id, IEnumerable<IDomainEvent> domainEvents);
        Task SaveEventsAsync<T>(Guid id, IEnumerable<IDomainEvent> domainEvents, long aggregateVersion);
        Task<AggregateEventList> RetrieveEventsAsync<T>(Guid id);
    }
}
