using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DomainModeling.Core;
using DomainModeling.Core.DomainObjectTypes;

namespace DomainModeling.EventSourcing
{
    public abstract class EventSourcedAggregate<T> : IEventSourcedAggregate<T> where T : AggregateRoot, new()
    {
        private readonly List<DomainEvent> _unrecordedEvents = new List<DomainEvent>();
        private readonly IEventDispatcher<T> _eventDispatcher;

        public IEnumerable<DomainEvent> UnrecordedEvents => _unrecordedEvents;
        public T State { get; }

        protected EventSourcedAggregate(Guid id, IEventDispatcher<T> eventDispatcher)
        {
            State = new T {Id = id};
            _eventDispatcher = eventDispatcher;
        }

        public abstract Task SaveAsync();

        public async Task FireEventAsync(DomainEvent @event)
        {
            _unrecordedEvents.Add(@event);
            await DispatchEventAsync(@event);
        }

        public async Task DispatchEventAsync(DomainEvent @event)
        {
            await _eventDispatcher.DispatchAsync(State, @event);
        }
    }
}