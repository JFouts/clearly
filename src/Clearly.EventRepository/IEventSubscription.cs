using System;
using System.Threading.Tasks;
using Clearly.Core.Interfaces;

namespace Clearly.EventRepository
{
    public interface IEventSubscription
    {
        Task Subscribe<T>(Func<IDomainEvent, Task> eventHandler);
    }
}