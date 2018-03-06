using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DomainModeling.Core.Interfaces;

namespace DomainModeling.EventSubscription
{
    public class SubscriptionDetail
    {
        public Type Type { get; set; }
        public IEventStream Stream { get; set; }
        public IDictionary<Type, Func<object, IDomainEvent,Task>> EventHandlers { get; set; } = new Dictionary<Type, Func<object, IDomainEvent, Task>>();
    }
}