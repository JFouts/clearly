﻿using System;
using System.Collections.Generic;
using Clearly.Core;
using Clearly.Core.Interfaces;

namespace Clearly.EventSourcing
{
    public class DomainEventRegistry<TAggregate> : IDomainEventRegistry<TAggregate> where TAggregate : AggregateRoot
    {
        private readonly Dictionary<Type, Type> _handlers = new Dictionary<Type, Type>();

        public Type GetHandlerType(Type eventType)
        {
            return _handlers[eventType];
        }

        public void RegisterHandler<TEvent, THandler>()
            where TEvent : IDomainEvent
            where THandler : IDomainEventHandler<TAggregate, TEvent>
        {
            _handlers[typeof(TEvent)] = typeof(THandler);
        }
    }
}
