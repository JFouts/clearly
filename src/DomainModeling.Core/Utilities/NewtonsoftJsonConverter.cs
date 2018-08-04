using System;
using DomainModeling.Core.Utilities.Interfaces;
using Newtonsoft.Json;

namespace DomainModeling.Core.Utilities {
    public class NewtonsoftJsonConverter : IJsonConverter {
        public string Serialize(object obj) => JsonConvert.SerializeObject(obj);
        public object Deserialize(string json) => JsonConvert.DeserializeObject(json);
        public object Deserialize(string json, Type type) => JsonConvert.DeserializeObject(json, type);
        public T Deserialize<T>(string json) => JsonConvert.DeserializeObject<T>(json);
    }
}