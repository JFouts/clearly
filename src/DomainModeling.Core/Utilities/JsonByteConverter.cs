using System;
using DomainModeling.Core.Utilities.Interfaces;

namespace DomainModeling.Core.Utilities
{
    public class JsonByteConverter : IJsonByteConverter
    {
        private readonly IJsonConverter _jsonConverter;
        private readonly IBinaryStringConverter _binaryStringConverter;

        public JsonByteConverter(IJsonConverter jsonConverter, IBinaryStringConverter binaryStringConverter)
        {
            _jsonConverter = jsonConverter;
            _binaryStringConverter = binaryStringConverter;
        }

        public byte[] Serialize(object obj)
        {
            return Encode(_jsonConverter.Serialize(obj));
        }

        public object Deserialize(byte[] data)
        {
            return _jsonConverter.Deserialize(Decode(data));
        }

        public object Deserialize(byte[] data, Type type)
        {
            return _jsonConverter.Deserialize(Decode(data), type);
        }

        public T Deserialize<T>(byte[] data)
        {
            return _jsonConverter.Deserialize<T>(Decode(data));
        }

        private byte[] Encode(string data)
        {
            return _binaryStringConverter.Encode(data);
        }

        private string Decode(byte[] data)
        {
            return _binaryStringConverter.Decode(data);
        }
    }
}