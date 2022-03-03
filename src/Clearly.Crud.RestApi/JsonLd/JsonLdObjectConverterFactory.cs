// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Text.Json;
using System.Text.Json.Serialization;
using Clearly.Core;
using Microsoft.Extensions.DependencyInjection;

namespace Clearly.Crud.JsonLd;

public class JsonLdObjectConverterFactory : JsonConverterFactory
{
    private IServiceProvider services;

    public JsonLdObjectConverterFactory(IServiceProvider services)
    {
        this.services = services;
    }

    public override bool CanConvert(Type typeToConvert)
    {
        return typeToConvert.IsAssignableTo(typeof(IEntity));
    }

    public override JsonConverter? CreateConverter(Type typeToConvert, JsonSerializerOptions options)
    {
        return services.GetRequiredService(typeof(JsonLdObjectConverter<>).MakeGenericType(typeToConvert)) as JsonConverter;
    }
}
