using System;
using System.Collections.Generic;
using System.Net;

namespace DomainModeling.EventRepository.EventStore
{
    public class EventStoreSettings
    {
        public IEventStoreNamingConvention NamingPreferences { get; set; } = new LiteralEventStoreNamingConvention();
        public IPEndPoint EndPoint { get; set; } = new IPEndPoint(IPAddress.Loopback, 1113);
        public IDictionary<string, Type> EventTypes { get; set; } = new Dictionary<string, Type>();
    }
}