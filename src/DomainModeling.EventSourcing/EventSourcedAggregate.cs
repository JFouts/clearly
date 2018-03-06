using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DomainModeling.Core;
using DomainModeling.Core.Interfaces;

namespace DomainModeling.EventSourcing
{
    public abstract class EventSourcedAggregate<T> : IEventSourcedAggregate<T> where T : AggregateRoot, new()
    {
        private readonly List<IDomainEvent> _unrecordedEvents = new List<IDomainEvent>();
        private readonly IEventDispatcher<T> _eventDispatcher;

        public IEnumerable<IDomainEvent> UnrecordedEvents => _unrecordedEvents;
        public T State { get; }

        protected EventSourcedAggregate(Guid id, IEventDispatcher<T> eventDispatcher)
        {
            State = new T {Id = id};
            _eventDispatcher = eventDispatcher;
        }

        public abstract Task SaveAsync();

        public async Task FireEventAsync(IDomainEvent @event)
        {
            _unrecordedEvents.Add(@event);
            await DispatchEventAsync(@event);
        }

        public async Task DispatchEventAsync(IDomainEvent @event)
        {
            await _eventDispatcher.DispatchAsync(State, @event);
        }
    }
}