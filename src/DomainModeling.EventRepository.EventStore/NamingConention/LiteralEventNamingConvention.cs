using System;
using System.Collections.Generic;

namespace DomainModeling.EventRepository.EventStore.NamingConention
{
    public class LiteralEventNamingConvention : IEventNamingConvention
    {
        private readonly Dictionary<string, Type> _registeredTypes = new Dictionary<string, Type>();

        public string GetEventName<T>()
        {
            return typeof(T).Name;
        }

        public string GetEventName(Type type)
        {
            return type.Name;
        }

        public Type GetEventType(string typeName)
        {
            if (_registeredTypes.ContainsKey(typeName))
                return _registeredTypes[typeName];

            throw new UnmappedEventNameException(typeName);
        }

        public bool IsKnownEventName(string eventName)
        {
            return _registeredTypes.ContainsKey(eventName);
        }

        public void AddEventType<T>()
        {
            var type = typeof(T);
            _registeredTypes[type.Name] = type;
        }
    }
}