﻿namespace DomainModeling.EventRepository.EventStore
{
    public static class EventStoreNamingConvention
    {
        public static IEventStoreNamingConvention Literal => new LiteralEventStoreNamingConvention();
    }
}
