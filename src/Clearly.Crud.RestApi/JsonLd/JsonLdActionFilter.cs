// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Clearly.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.DependencyInjection;

namespace Clearly.Crud.JsonLd;

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
            using var scope = context.HttpContext.RequestServices.CreateScope();
            objectResult.Formatters.Add(scope.ServiceProvider.GetRequiredService<SystemTextJsonLdOutputFormatter>());
            objectResult.Formatters.Add(scope.ServiceProvider.GetRequiredService<SystemTextJsonOutputFormatter>());
        }
    }
}
