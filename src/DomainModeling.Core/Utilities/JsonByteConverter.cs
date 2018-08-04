using System;
using DomainModeling.Core.Utilities.Interfaces;

namespace DomainModeling.Core.Utilities {
    public class JsonByteConverter : IJsonByteConverter {
        private readonly IJsonConverter jsonConverter;
        private readonly IBinaryStringConverter binaryStringConverter;

        public JsonByteConverter(IJsonConverter jsonConverter, IBinaryStringConverter binaryStringConverter) {
            this.jsonConverter = jsonConverter;
            this.binaryStringConverter = binaryStringConverter;
        }

        public byte[] Serialize(object obj) => Encode(jsonConverter.Serialize(obj));
        public object Deserialize(byte[] data) => jsonConverter.Deserialize(Decode(data));
        public object Deserialize(byte[] data, Type type) => jsonConverter.Deserialize(Decode(data), type);
        public T Deserialize<T>(byte[] data) => jsonConverter.Deserialize<T>(Decode(data));
        private byte[] Encode(string data) => binaryStringConverter.Encode(data);
        private string Decode(byte[] data) => binaryStringConverter.Decode(data);
    }
}