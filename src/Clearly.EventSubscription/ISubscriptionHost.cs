using System;
using System.Threading.Tasks;
using Clearly.Core.Interfaces;

namespace Clearly.EventSubscription
{
    public interface ISubscriptionHost
    {
        Task StartAsync();
        void Subscribe(IEventStream stream, Func<IDomainEvent, Task> eventHandler);
    }
}