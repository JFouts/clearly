using System;

namespace DomainModeling.EventRepository.EventStore.NamingConention
{
    public class LiteralStreamNamingConvention : IStreamNamingConvention
    {
        public string GetStreamName(Type type)
        {
            return type.Name;
        }

        public string GetStreamName<T>()
        {
            return typeof(T).Name;
        }
    }
}