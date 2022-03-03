using System;
using System.Collections.Generic;

namespace Clearly.EventRepository.EventStore.NamingConvention
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

        public void AddEventType<T>()
        {
            var type = typeof(T);
            _registeredTypes[type.Name] = type;
        }
    }
}