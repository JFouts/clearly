using System;
using System.Threading.Tasks;
using DomainModeling.Core.Interfaces;

namespace DomainModeling.EventSubscription
{
    public interface ISubscriptionHost
    {
        Task StartAsync();
        void Subscribe(IEventStream stream, Func<IDomainEvent, Task> eventHandler);
    }
}