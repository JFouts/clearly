// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Reflection;
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

    /// <summary>
    /// Adds all the required service and initializes MVC Controllers for a Clearly CRUD API
    /// </summary>
    /// <param name="assemblies">Assemblies containing Entity defintions.</param>
    /// <returns>IMvcBuilder from AddControllers()</returns>
    /// <remarks>
    /// This will automatically configure AddControllers(), to manually configure MVC in your project's startup
    /// see TODO: Link to documentation
    /// </remarks>
    public static IMvcBuilder AddCrudRestApi(this IServiceCollection services, params Assembly[] assemblies)
    {
        return AddCrudRestApiInternal(services, null, assemblies);
    }

    /// <summary>
    /// All required service and initializes MVC Controllers for a Clearly CRUD API
    /// </summary>
    /// <param name="assemblies">Assemblies containing Entity defintions.</param>
    /// <returns>IMvcBuilder from AddControllers()</returns>
    /// <remarks>
    /// This will automatically configure AddControllers(), to manually configure MVC in your project's startup
    /// see TODO: Link to documentation
    /// </remarks>
    public static IMvcBuilder AddCrudRestApi(this IServiceCollection services, Action<MvcOptions> configure, params Assembly[] assemblies)
    {
        return AddCrudRestApiInternal(services, configure, assemblies);
    }

    /// <summary>
    /// Adds the required services for Clearly CRUD's API
    /// </summary>
    /// <remarks>
    /// Typically you should use AddCrudRestApi. Call this method only if you are manually configuring MVC for example if you are running
    /// the Clearly CRUD API side by side with another API for application with MVC Views or Razor pages.
    /// </remarks>
    public static IServiceCollection AddCrudRestApiServices(this IServiceCollection services)
    {
        services
            .AddSingleton<JsonLdActionFilter>()
            .AddSingleton<JsonLdObjectConverterFactory>()
            .AddSingleton(typeof(JsonLdObjectConverter<>));

        // TODO: I hate this, it feels so against how aspnetcore want you to setup Formatters, but we need EntityDefinitions in our formatter
        // Look into ways to rework this so that the DI container doesn't need to be fully initialized when we create our Formatter
        services.AddScoped<SystemTextJsonLdOutputFormatter>(x => 
            new SystemTextJsonLdOutputFormatter(
                x.GetRequiredService<IOptionsSnapshot<JsonOptions>>().Get("jsonld").JsonSerializerOptions, 
                x.GetRequiredService<JsonLdObjectConverterFactory>()));
        services.AddScoped<SystemTextJsonOutputFormatter>(x => 
            new SystemTextJsonOutputFormatter(
                x.GetRequiredService<IOptions<JsonOptions>>().Value.JsonSerializerOptions));

        return services
            .AddCrudServices();
    }

    private static IMvcBuilder AddCrudRestApiInternal(this IServiceCollection services, Action<MvcOptions>? configure, params Assembly[] assemblies)
    {
        return services
            .AddCrudRestApiServices()
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
}
