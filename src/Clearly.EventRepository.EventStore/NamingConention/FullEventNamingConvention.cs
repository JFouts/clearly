using System;

namespace Clearly.EventRepository.EventStore.NamingConvention
{
    public class FullEventNamingConvention : IEventNamingConvention
    {
        public string GetEventName<T>()
        {
            return typeof(T).FullName!;
        }

        public string GetEventName(Type type)
        {
            return type.FullName!;
        }

        public Type GetEventType(string typeName)
        {
            return Type.GetType(typeName) ?? throw new KeyNotFoundException($"No Type with name {typeName} was found.");
        }
    }
}