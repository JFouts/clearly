// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Clearly.Core.Utilities.Interfaces;
using Newtonsoft.Json;

namespace Clearly.Core.Utilities;

public class NewtonsoftJsonConverter : IJsonConverter
{
    public string Serialize(object obj)
    {
        return JsonConvert.SerializeObject(obj);
    }

    public object Deserialize(string json)
    {
        var response = JsonConvert.DeserializeObject(json);

        if (response == null)
        {
            throw new JsonSerializationException("Invalid JSON, failed to deserialize.");
        }

        return response;
    }

    public object Deserialize(string json, Type type)
    {
        var response =  JsonConvert.DeserializeObject(json, type);

        if (response == null)
        {
            throw new JsonSerializationException("Invalid JSON, failed to deserialize.");
        }

        return response;
    }

    public T Deserialize<T>(string json)
    {
        var response = JsonConvert.DeserializeObject<T>(json);

        if (response == null)
        {
            throw new JsonSerializationException("Invalid JSON, failed to deserialize.");
        }

        return response;
    }
}
