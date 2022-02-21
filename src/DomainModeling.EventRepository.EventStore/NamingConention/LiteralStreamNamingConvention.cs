using System;

namespace DomainModeling.EventRepository.EventStore.NamingConvention
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