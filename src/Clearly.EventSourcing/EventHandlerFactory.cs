using Clearly.Core;
using Microsoft.Extensions.DependencyInjection;

namespace Clearly.EventSourcing;

public class EventHandlerFactory<TAggregate> : IEventHandlerFactory<TAggregate>
    where TAggregate : AggregateRoot
{
    private readonly IDomainEventRegistry<TAggregate> domainEventRegistry;
    private readonly IServiceProvider serviceProvider;

    public EventHandlerFactory(IServiceProvider serviceProvider, IDomainEventRegistry<TAggregate> domainEventRegistry)
    {
        this.serviceProvider = serviceProvider;
        this.domainEventRegistry = domainEventRegistry;
    }

    public IDomainEventHandler<TAggregate> CreateHandler(Type eventType)
    {
        var handlerType = domainEventRegistry.GetHandlerType(eventType);
        var handler = serviceProvider.GetRequiredService(handlerType) as IDomainEventHandler<TAggregate>;

        if (handler == null)
        {
            throw new ArgumentException($"Handler for {eventType.FullName} is not of type {nameof(IDomainEventHandler<TAggregate>)}", nameof(eventType));
        }

        return handler;
    }
}
