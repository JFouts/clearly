// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Text.Json;
using DomainModeling.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace DomainModeling.Crud.JsonLd;

public class JsonLdActionFilter : IAsyncActionFilter
{
    private readonly JsonLdObjectConverterFactory jsonLdObjectConverterFactory;

    public JsonLdActionFilter(JsonLdObjectConverterFactory jsonLdObjectConverterFactory)
    {
        this.jsonLdObjectConverterFactory = jsonLdObjectConverterFactory;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var executedContext = await next();

        if (executedContext.Result is ObjectResult objectResult && objectResult.Value is IEntity entity)
        {
            objectResult.Formatters.Add(new SystemTextJsonLdOutputFormatter(new JsonSerializerOptions(), jsonLdObjectConverterFactory));
            objectResult.Formatters.Add(new SystemTextJsonOutputFormatter(new JsonSerializerOptions()));
        }
    }
}
