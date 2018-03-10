using System;

namespace DomainModeling.EventRepository.EventStore.NamingConention
{
    public interface IEventNamingConvention
    {
        string GetEventName<T>();
        string GetEventName(Type type);
        Type GetEventType(string typeName);
    }
}