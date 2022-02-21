using System;

namespace DomainModeling.EventRepository.EventStore.NamingConvention
{
    public interface IStreamNamingConvention
    {
        string GetStreamName<T>();
        string GetStreamName(Type type);
    }
}