// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Text.Json;
using DomainModeling.Core;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.DependencyInjection;

namespace DomainModeling.Crud.JsonLd;

/// <summary>
/// A <see cref="TextOutputFormatter"/> for JSON-LD content that uses <see cref="JsonSerializer"/>.
/// </summary>
public class SystemTextJsonLdOutputFormatter : SystemTextJsonOutputFormatter
{
    private readonly JsonLdObjectConverterFactory converterFactory;

    /// <summary>
    /// Initializes a new <see cref="SystemTextJsonOutputFormatter"/> instance.
    /// </summary>
    /// <param name="jsonSerializerOptions">The <see cref="JsonSerializerOptions"/>.</param>
    public SystemTextJsonLdOutputFormatter(JsonSerializerOptions serializerOptions, JsonLdObjectConverterFactory converterFactory)
        : base(new JsonSerializerOptions(serializerOptions))
    {
        this.converterFactory = converterFactory;

        SerializerOptions.Converters.Insert(0, converterFactory);

        SupportedMediaTypes.Clear();
        SupportedMediaTypes.Add("application/ld+json");
    }

    public override bool CanWriteResult(OutputFormatterCanWriteContext context)
    {
        if (!context.ContentType.Equals("application/ld+json", StringComparison.InvariantCultureIgnoreCase))
        {
            return false;
        }

        if (!base.CanWriteResult(context))
        {
            return false;
        }

        var factory = context.HttpContext.RequestServices.GetRequiredService<IEntityDefinitionFactory>();
        var entity = factory.CreateForType(context.GetType());

        // TODO: Better checking for if Type Definition can be used for JSON-LD
        return true;
    }
    
    protected override bool CanWriteType(Type? type)
    {
        return type != null 
            && base.CanWriteType(type) 
            && type.IsAssignableTo(typeof(IEntity));
    }
}
