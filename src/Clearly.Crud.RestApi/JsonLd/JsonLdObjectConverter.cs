// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using Clearly.Core;

namespace Clearly.Crud.JsonLd;

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

        var typeJsonMetadata = definition.Using<JsonLdTypeFeature>();
        var vocab = $"{baseUrl}/schema/{nameKey}#";

        // TODO: Move this fallback logic to defintion generation
        if (!string.IsNullOrWhiteSpace(typeJsonMetadata.TermsDefaultVocab))
        {
            vocab = typeJsonMetadata.TermsDefaultVocab;
        }

        var typeIri = $"{baseUrl}/schema/{nameKey}";

        // TODO: Move this fallback logic to defintion generation
        if (!string.IsNullOrWhiteSpace(typeJsonMetadata.TypeIri))
        {
            typeIri = typeJsonMetadata.TypeIri;
        }

        responseBody["@context"] = ldContext;

        writer.WriteStartObject();
        writer.WriteStartObject("@context");
        writer.WriteNumber("@version", 1.1);
        writer.WriteString("@vocab", vocab);

        foreach (var field in definition.Fields)
        {
            var fieldJsonMetadata = field.Using<JsonLdFieldFeature>();
            var propertyToken = options.PropertyNamingPolicy?.ConvertName(field.Property.Name) ?? field.Property.Name;

            if (fieldJsonMetadata.ExcludeFromLinkedData)
            {
                writer.WriteString(propertyToken, string.Empty);
            }
            else if (!string.IsNullOrWhiteSpace(fieldJsonMetadata.Iri))
            {
                ldContext[propertyToken] = fieldJsonMetadata.Iri;

                writer.WriteString(propertyToken, fieldJsonMetadata.Iri);
            }
        }

        writer.WriteEndObject(); // End of @context

        writer.WriteString("@type", typeIri);
        writer.WriteString("@id", $"{baseUrl}/api/{nameKey}/{value.Id}"); // TODO: Should be set of defintion
        
        foreach (var field in definition.Fields)
        {
            var fieldJsonMetadata = field.Using<JsonLdFieldFeature>();
            var propertyToken = options.PropertyNamingPolicy?.ConvertName(field.Property.Name) ?? field.Property.Name;

            writer.WritePropertyName(propertyToken);
            JsonSerializer.Serialize(writer, field.Property.GetValue(value), options);
        }

        writer.WriteEndObject(); // End of main object
    }
}
