using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DomainModeling.Core.DomainObjectTypes;

namespace DomainModeling.EventRepository
{
    public interface IEventRepository
    {
        Task SaveNewStreamEventsAsync<T>(Guid id, IEnumerable<DomainEvent> domainEvents);
        Task SaveEventsAsync<T>(Guid id, IEnumerable<DomainEvent> domainEvents, long aggregateVersion);
        Task<AggregateEventList> RetriveEventsAsync<T>(Guid id);
    }
}
