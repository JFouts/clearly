// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Dynamic;
using DomainModeling.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DomainModeling.Crud.JsonLd;

public class JsonLdActionFilter : IAsyncActionFilter
{    
    private readonly IEntityDefinitionFactory entityDefinitionFactory;

    public JsonLdActionFilter(IEntityDefinitionFactory entityDefinitionFactory)
    {
        this.entityDefinitionFactory = entityDefinitionFactory;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var executedContext = await next();

        if (executedContext.Result is ObjectResult objectResult && objectResult.Value is IEntity entity)
        {
            // TODO: Configurable base URL or none at all
            var baseUrl = $"http{(context.HttpContext.Request.IsHttps ? "s" : "")}://{context.HttpContext.Request.Host}";

            var type = entity.GetType();
            var definition = entityDefinitionFactory.CreateForType(type);

            dynamic responseBody = new ExpandoObject();
            IDictionary<string, object?> responseBodyDictionary = responseBody;

            var nameKey = definition.NameKey.ToLower();

            responseBodyDictionary["@context"] = $"{baseUrl}/schema/{nameKey}";
            responseBodyDictionary["@type"] = $"{baseUrl}/schema/{nameKey}";
            responseBodyDictionary["@id"] = $"{baseUrl}/api/{nameKey}/{entity.Id}"; // TODO: Should be set of defintion

            var ldContext = new Dictionary<string, object>();
            ldContext["@version"] = 1.1;

            foreach (var field in definition.Fields)
            {
                var fieldJsonMetadata = field.UsingMetadata<JsonLdFieldMetadata>();
                var propertyToken = field.Property.Name.ToCamelCase();

                if (!string.IsNullOrWhiteSpace(fieldJsonMetadata.Iri))
                {
                    ldContext[propertyToken] = fieldJsonMetadata.Iri;
                }
                else
                {
                    ldContext[propertyToken] = $"{baseUrl}/schema/{nameKey}#{propertyToken.ToLower()}";
                }

                responseBodyDictionary[propertyToken] = field.Property.GetValue(entity);
            }

            responseBodyDictionary["@context"] = ldContext;

            objectResult.Value = responseBody;
            objectResult.DeclaredType = responseBody.GetType();
        }
    }

    private IEnumerable<HateoasLink> GenerateLinks(EntityDefinition definition, IEntity entity)
    {
        var apis = definition.Features.OfType<EntityApiFeature>();

        foreach (var api in apis)
        {
            yield return new HateoasLink("self", api.UrlFormat, api.Method);
        }
    }
}
