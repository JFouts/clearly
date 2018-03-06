using System;

namespace DomainModeling.EventRepository.EventStore
{
    public interface IEventStoreNamingConvention
    {
        string GetNamespaceName<T>();
        string GetNamespaceName(Type type);
        string GetEventTypeName<T>();
        string GetEventTypeName(Type type);
    }
}
