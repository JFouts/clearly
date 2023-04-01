// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Text.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Clearly.Crud.EntityGraph;

// TODO: This belongs somewhere else
public class JTokenJsonConverter : System.Text.Json.Serialization.JsonConverter<JToken>
{
    public override JToken Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return JToken.Parse(reader.GetString() ?? string.Empty);
    }

    public override void Write(Utf8JsonWriter writer, JToken value, JsonSerializerOptions options)
    {
        writer.WriteRawValue(value.ToString(options.WriteIndented ? Formatting.Indented : Formatting.None));
    }
}
