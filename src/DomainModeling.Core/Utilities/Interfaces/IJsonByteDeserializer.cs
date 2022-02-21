// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace DomainModeling.Core.Utilities.Interfaces;

public interface IJsonByteDeserializer
{
    object Deserialize(byte[] data);
    object Deserialize(byte[] data, Type type);
    T Deserialize<T>(byte[] data);
}
