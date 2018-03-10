using System;

namespace DomainModeling.EventRepository.EventStore.NamingConention
{
    public interface IStreamNamingConvention
    {
        string GetStreamName<T>();
        string GetStreamName(Type type);
    }
}