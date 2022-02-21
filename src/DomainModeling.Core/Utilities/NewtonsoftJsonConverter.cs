// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using DomainModeling.Core.Utilities.Interfaces;
using Newtonsoft.Json;

namespace DomainModeling.Core.Utilities;

public class NewtonsoftJsonConverter : IJsonConverter
{
    public string Serialize(object obj)
    {
        return JsonConvert.SerializeObject(obj);
    }

    public object Deserialize(string json)
    {
        return JsonConvert.DeserializeObject(json);
    }

    public object Deserialize(string json, Type type)
    {
        return JsonConvert.DeserializeObject(json, type);
    }

    public T Deserialize<T>(string json)
    {
        return JsonConvert.DeserializeObject<T>(json);
    }
}
