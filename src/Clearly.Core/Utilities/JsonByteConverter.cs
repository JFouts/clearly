// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Clearly.Core.Utilities.Interfaces;

namespace Clearly.Core.Utilities;

public class JsonByteConverter : IJsonByteConverter
{
    private readonly IJsonConverter jsonConverter;
    private readonly IBinaryStringConverter binaryStringConverter;

    public JsonByteConverter(IJsonConverter jsonConverter, IBinaryStringConverter binaryStringConverter)
    {
        this.jsonConverter = jsonConverter;
        this.binaryStringConverter = binaryStringConverter;
    }

    public byte[] Serialize(object obj)
    {
        return Encode(jsonConverter.Serialize(obj));
    }

    public object Deserialize(byte[] data)
    {
        return jsonConverter.Deserialize(Decode(data));
    }

    public object Deserialize(byte[] data, Type type)
    {
        return jsonConverter.Deserialize(Decode(data), type);
    }

    public T Deserialize<T>(byte[] data)
    {
        return jsonConverter.Deserialize<T>(Decode(data));
    }

    private byte[] Encode(string data)
    {
        return binaryStringConverter.Encode(data);
    }

    private string Decode(byte[] data)
    {
        return binaryStringConverter.Decode(data);
    }
}
