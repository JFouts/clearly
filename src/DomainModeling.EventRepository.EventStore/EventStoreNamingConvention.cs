using DomainModeling.EventRepository.EventStore.NamingConention;

namespace DomainModeling.EventRepository.EventStore
{
    public static class SteamNamingConvention
    {
        public static IStreamNamingConvention Literal => new LiteralStreamNamingConvention();
    }

    public static class EventNamingConvention
    {
        public static IEventNamingConvention Literal => new LiteralEventNamingConvention();
        public static IEventNamingConvention FullyQualified => new FullEventNamingConvention();
        public static IEventNamingConvention Custom => new CustomEventNamingConvention();
    }
}
