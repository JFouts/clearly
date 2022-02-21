using System;

namespace DomainModeling.EventRepository.EventStore.NamingConvention
{
    public interface IEventNamingConvention
    {
        string GetEventName<T>();
        string GetEventName(Type type);
        Type GetEventType(string typeName);
    }
}