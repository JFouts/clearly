using System;
using DomainModeling.Core;

namespace DomainModeling.EventSourcing
{
    public class EventHandlerFactory<TAggregate> : IEventHandlerFactory<TAggregate> where TAggregate : AggregateRoot
    {
        private readonly IDomainEventRegistry<TAggregate> _domainEventRegistry;
        private readonly IServiceProvider _serviceProvider;

        public EventHandlerFactory(IServiceProvider serviceProvider, IDomainEventRegistry<TAggregate> domainEventRegistry)
        {
            _serviceProvider = serviceProvider;
            _domainEventRegistry = domainEventRegistry;
        }

        public IDomainEventHandler<TAggregate> CreateHandler(Type eventType)
        {
            var handlerType = _domainEventRegistry.GetHandlerType(eventType);
            var handler = _serviceProvider.GetService(handlerType) as IDomainEventHandler<TAggregate>;
            return handler;
        }
    }
}
