using Clearly.Core;
using Clearly.Core.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Clearly.EventSourcing;

public class AggregateFactory<TAggregate> : IAggregateFactory<TAggregate>
    where TAggregate : AggregateRoot, new()
{
    private readonly IServiceProvider serviceProvider;
    private readonly IEventDispatcher<TAggregate> eventDispatcher;

    public AggregateFactory(IServiceProvider serviceProvider, IEventDispatcher<TAggregate> eventDispatcher)
    {
        this.serviceProvider = serviceProvider;
        this.eventDispatcher = eventDispatcher;
    }

    public EventSourcedAggregate<TAggregate> CreateUnrecordedAggregate(Guid id)
    {
        var repo = serviceProvider.GetRequiredService<IEventSourcedAggregateRepository<TAggregate>>();

        return new UnrecordedAggregate<TAggregate>(id, repo, eventDispatcher);
    }

    public EventSourcedAggregate<TAggregate> CreateRecordedAggregate(Guid id, long version, IEnumerable<IDomainEvent> events)
    {
        var repo = serviceProvider.GetRequiredService<IEventSourcedAggregateRepository<TAggregate>>();

        return new RecordedAggregate<TAggregate>(id, version, events, repo, eventDispatcher);
    }
}
