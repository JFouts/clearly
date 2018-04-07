using System.Net;
using DomainModeling.EventRepository.EventStore.NamingConention;

namespace DomainModeling.EventRepository.EventStore
{
    public class EventStoreSettings
    {
        public IStreamNamingConvention StreamNamingPreferences { get; set; } = SteamNamingConvention.Literal;
        public IEventNamingConvention EventNamingPreferences { get; set; } = EventNamingConvention.FullyQualified;
        public IPEndPoint EndPoint { get; set; } = new IPEndPoint(IPAddress.Loopback, 1113);
    }
}