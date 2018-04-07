using System;
using System.Reflection;

namespace DomainModeling.EventRepository.EventStore.NamingConention
{
    public class FullEventNamingConvention : IEventNamingConvention
    {
        private Assembly _typesAssembly = Assembly.Load("Questionable.Questions.Events");

        public string GetEventName<T>()
        {
            return typeof(T).FullName;
        }

        public string GetEventName(Type type)
        {
            return type.FullName;
        }

        public Type GetEventType(string typeName)
        {
            return _typesAssembly.GetType(typeName);
        }

        public bool IsKnownEventName(string eventName)
        {
            return _typesAssembly.GetType(eventName) != null;
        }
    }
}