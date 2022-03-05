using System.Collections.Generic;
using System.Threading.Tasks;
using Clearly.Core;
using Clearly.Core.Interfaces;
using Clearly.EventSourcing;

namespace Questionable.Questions.Test.Unit;

public class MockAggregate<T> : IEventSourcedAggregate<T>
    where T : AggregateRoot, new()
{
    private readonly List<IDomainEvent> unrecordedEvents = new List<IDomainEvent>();

    public List<IDomainEvent> SavedEvents { get; } = new List<IDomainEvent>();
    public IEnumerable<IDomainEvent> UnrecordedEvents { get; } = new List<IDomainEvent>();

    public T State { get; set; } = new T();

    public Task DispatchEventAsync(IDomainEvent @event)
    {
        return Task.CompletedTask;
    }

    public Task FireEventAsync(IDomainEvent @event)
    {
        unrecordedEvents.Add(@event);
        
        return Task.CompletedTask;
    }

    public Task SaveAsync()
    {
        SavedEvents.AddRange(unrecordedEvents);
        unrecordedEvents.Clear();

        return Task.CompletedTask;
    }
}
