using System;
using System.Threading.Tasks;
using DomainModeling.Core.DomainObjectTypes;

namespace DomainModeling.EventRepository
{
    public interface IEventSubscription
    {
        Task Subscribe<T>(Func<DomainEvent, Task> eventHandler);
    }
}