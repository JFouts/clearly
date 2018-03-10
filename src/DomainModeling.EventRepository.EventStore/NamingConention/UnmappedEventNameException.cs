using System;

namespace DomainModeling.EventRepository.EventStore.NamingConention
{
    public class UnmappedEventNameException : Exception
    {
        public string EventName { get; }

        public UnmappedEventNameException(string eventName)
        {
            EventName = eventName;
        }
    }
}