using System;
using System.Collections.Generic;

namespace Clearly.EventRepository.EventStore.NamingConvention
{
    public class CustomEventNamingConvention : IEventNamingConvention
    {
        private readonly Dictionary<Type, string> _typeMapping = new Dictionary<Type, string>();
        private readonly Dictionary<string, Type> _nameMapping = new Dictionary<string, Type>();

        public string GetEventName<T>()
        {
            return GetEventName(typeof(T));
        }

        public string GetEventName(Type type)
        {
            if(!_typeMapping.ContainsKey(type))
                throw new UnmappedTypeException();

            return _typeMapping[type];
        }

        public Type GetEventType(string typeName)
        {
            if (!_nameMapping.ContainsKey(typeName))
                throw new UnmappedTypeException();

            return _nameMapping[typeName];
        }

        public void AddMap(Type eventType, string typeName)
        {
            _typeMapping[eventType] = typeName;
            _nameMapping[typeName] = eventType;
        }
    }
}