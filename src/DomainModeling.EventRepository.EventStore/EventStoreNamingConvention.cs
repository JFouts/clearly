namespace DomainModeling.EventRepository.EventStore
{
    public static class NamingConvention
    {
        public static IEventStoreNamingConvention Literal => new LiteralEventStoreNamingConvention();
    }
}
