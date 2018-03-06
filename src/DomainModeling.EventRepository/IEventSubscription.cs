using System;
using System.Threading.Tasks;
using DomainModeling.Core.Interfaces;

namespace DomainModeling.EventRepository
{
    public interface IEventSubscription
    {
        Task Subscribe<T>(Func<IDomainEvent, Task> eventHandler);
    }
}