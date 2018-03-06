using System;

namespace DomainModeling.EventRepository.EventStore
{
    public class LiteralEventStoreNamingConvention : IEventStoreNamingConvention
    {
        public string GetNamespaceName<T>()
        {
            return GetNamespaceName(typeof(T));
        }

        public string GetNamespaceName(Type type)
        {
            return type.Name;
        }

        public string GetEventTypeName<T>()
        {
            return GetEventTypeName(typeof(T));
        }

        public string GetEventTypeName(Type type)
        {
            return type.Name;
        }
    }
}
