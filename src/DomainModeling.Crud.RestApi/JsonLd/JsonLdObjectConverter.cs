// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using DomainModeling.Core;

namespace DomainModeling.Crud.JsonLd;

public class JsonLdObjectConverter<TEntity> : JsonConverter<TEntity>
    where TEntity : IEntity
{
    private IEntityDefinitionFactory entityDefinitionFactory;

    public JsonLdObjectConverter(IEntityDefinitionFactory entityDefinitionFactory)
    {
        this.entityDefinitionFactory = entityDefinitionFactory;
    }

    public override TEntity? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, TEntity value, JsonSerializerOptions options)
    {
        // TODO: Configurable base URL or none at all
        var baseUrl = string.Empty; // $"http{(context.HttpContext.Request.IsHttps ? "s" : "")}://{context.HttpContext.Request.Host}";

        var type = value.GetType();
        var definition = entityDefinitionFactory.CreateForType(type);

        // TODO: Consider switching to a mutable JsonDocument
        var ldContext = new JsonObject();
        var responseBody = new JsonObject();

        // TODO: Consider this being already Lower on the definition
        var nameKey = definition.NameKey.ToLower();

        responseBody["@context"] = ldContext;

        writer.WriteStartObject();
        writer.WritePropertyName("@context");
        writer.WriteStartObject();
        writer.WriteNumber("@version", 1.1);
        writer.WriteString("@vocab", $"{baseUrl}/schema/{nameKey}#");

        foreach (var field in definition.Fields)
        {
            var fieldJsonMetadata = field.UsingMetadata<JsonLdFieldMetadata>();

            if (!string.IsNullOrWhiteSpace(fieldJsonMetadata.Iri))
            {
                var propertyToken = field.Property.Name.ToCamelCase(); // TODO: Support casing options
                ldContext[propertyToken] = fieldJsonMetadata.Iri;

                writer.WriteString(propertyToken, fieldJsonMetadata.Iri);
            }
        }

        writer.WriteEndObject(); // End of @context

        writer.WriteString("@type", $"{baseUrl}/schema/{nameKey}");
        writer.WriteString("@id", $"{baseUrl}/api/{nameKey}/{value.Id}"); // TODO: Should be set of defintion
        
        foreach (var field in definition.Fields)
        {
            var fieldJsonMetadata = field.UsingMetadata<JsonLdFieldMetadata>();
            var propertyToken = field.Property.Name.ToCamelCase(); // TODO: Support casing options

            writer.WritePropertyName(propertyToken);
            JsonSerializer.Serialize(writer, field.Property.GetValue(value), options);
        }

        writer.WriteEndObject(); // End of object
    }
}
