using System.Collections.Generic;
using System.Threading.Tasks;
using DomainModeling.Core;
using DomainModeling.Core.Interfaces;
using DomainModeling.EventSourcing;

namespace Questionable.Questions.Test.Unit
{
    public class MockAggregate<T> : IEventSourcedAggregate<T> where T : AggregateRoot
    {
        private readonly List<IDomainEvent> _uncrecorededEvents = new List<IDomainEvent>();

        public List<IDomainEvent> SavedEvents { get; } = new List<IDomainEvent>();
        public IEnumerable<IDomainEvent> UnrecordedEvents { get; } = new List<IDomainEvent>();

        public T State { get; set; }

        public Task DispatchEventAsync(IDomainEvent @event)
        {
            return Task.CompletedTask;
        }

        public Task FireEventAsync(IDomainEvent @event)
        {
            _uncrecorededEvents.Add(@event);
            return Task.CompletedTask;
        }

        public Task SaveAsync()
        {
            SavedEvents.AddRange(_uncrecorededEvents);
            _uncrecorededEvents.Clear();
            return Task.CompletedTask;
        }
    }
}