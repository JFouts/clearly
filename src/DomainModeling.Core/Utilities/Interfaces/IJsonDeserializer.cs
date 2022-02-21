// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace DomainModeling.Core.Utilities.Interfaces;

public interface IJsonDeserializer
{
    object Deserialize(string json);
    object Deserialize(string json, Type type);
    T Deserialize<T>(string json);
}
