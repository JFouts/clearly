using System;

namespace DomainModeling.Core.Utilities.Interfaces
{
    public interface IJsonByteDeserializer
    {
        object Deserialize(byte[] data);
        object Deserialize(byte[] data, Type type);
        T Deserialize<T>(byte[] data);
    }
}