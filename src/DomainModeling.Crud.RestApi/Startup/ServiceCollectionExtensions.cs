// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Reflection;
using System.Text.Encodings.Web;
using System.Text.Json;
using DomainModeling.Crud.JsonLd;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace DomainModeling.Crud.RestApi;


public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Configures a named <see cref="JsonOptions"/> for the specified <paramref name="builder"/>.
    /// </summary>
    /// <param name="builder">The <see cref="IMvcBuilder"/>.</param>
    /// <param name="name">Name of the <see cref="JsonOptions"/> to configure.</param>
    /// <param name="configure">An <see cref="Action"/> to configure the <see cref="JsonOptions"/>.</param>
    /// <returns>The <see cref="IMvcBuilder"/>.</returns>
    public static IMvcBuilder AddJsonOptions(this IMvcBuilder builder, string name, Action<JsonOptions> configure)
    {
        ArgumentNullException.ThrowIfNull(builder, nameof(builder));
        ArgumentNullException.ThrowIfNull(name, nameof(name));
        ArgumentNullException.ThrowIfNull(configure, nameof(configure));

        builder.Services.Configure(name, configure);
        return builder;
    }

    public static IMvcBuilder AddCrudRestApi(this IServiceCollection services, params Assembly[] assemblies)
    {
        return AddCrudRestApiInternal(services, null, assemblies);
    }

    
    public static IMvcBuilder AddCrudRestApi(this IServiceCollection services, Action<MvcOptions> configure, params Assembly[] assemblies)
    {
        return AddCrudRestApiInternal(services, configure, assemblies);
    }

    internal static IMvcBuilder AddCrudRestApiInternal(this IServiceCollection services, Action<MvcOptions>? configure, params Assembly[] assemblies)
    {
        services.AddSingleton<JsonLdActionFilter>();
        services.AddSingleton<JsonLdObjectConverterFactory>();
        services.AddSingleton(typeof(JsonLdObjectConverter<>));

        // services.AddOptions<JsonOptions>("jsonld").Validate(x => x.JsonSerializerOptions.Converters.Any(c => c is JsonLdObjectConverterFactory));

        // TODO: I hate this, it feels so against how aspnetcore want you to setup Formatters, but we need EntityDefinitions in our formatter
        // Look into ways to rework this so that the DI container doesn't need to be fully initialized when we create our Formatter
        services.AddScoped<SystemTextJsonLdOutputFormatter>(x => 
            new SystemTextJsonLdOutputFormatter(
                x.GetRequiredService<IOptionsSnapshot<JsonOptions>>().Get("jsonld").JsonSerializerOptions 
                // ?? 
                // x.GetRequiredService<IOptionsSnapshot<JsonOptions>>().Value?.JsonSerializerOptions ??
                // new JsonSerializerOptions(JsonSerializerDefaults.Web)
                // {
                //     Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                // }
                , 
                x.GetRequiredService<JsonLdObjectConverterFactory>()));
        services.AddScoped<SystemTextJsonOutputFormatter>(x => 
            new SystemTextJsonOutputFormatter(
                x.GetRequiredService<IOptions<JsonOptions>>().Value.JsonSerializerOptions
                // ??
                // new JsonSerializerOptions(JsonSerializerDefaults.Web)
                // {
                //     Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                // }
            ));

        return services
            .AddCrudServices()
            .AddInMemoryEntityRepository() // TODO: Replace this
            .AddControllers(o =>
            {
                o.AddCrudConvention();
                if (configure != null)
                {
                    configure(o);
                }
            })
            .AddCrudFeature(assemblies);
    }

    // Option 1 - I only want to use the CRUD services/repositories
    // services
    //     .AddCrudServices()
    //     .AddInMemoryEntityRepository(); // Some Entity Repository Implementation

    // Option 2 - I want to use the CRUD REST API standalone
    // services.AddCrudRestApi();

    // Option 3 - I want to run the CRUD REST API in the same app a Razor/Views application
    // services
    //     .AddCrudServices()
    //     .AddInMemoryEntityRepository(); // Some Entity Repository Implementation
    //
    // services
    //     .AddMvc(o =>   // Or AddControllersWithViews
    //          o.AddCrudConvention())
    //     .AddCrudFeatures();

    // Option 4 - I want to run the CRUD REST API along side the CRUD Web UI
    // TODO

    // Option 5 - I want to run the CRUD Web UI standalone
    // TODO
}
