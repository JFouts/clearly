using System;

namespace DomainModeling.Core.Utilities.Interfaces
{
    public interface IJsonDeserializer
    {
        object Deserialize(string json);
        object Deserialize(string json, Type type);
        T Deserialize<T>(string json);
    }
}