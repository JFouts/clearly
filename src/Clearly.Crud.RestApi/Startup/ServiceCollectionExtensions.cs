// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Reflection;
using Clearly.Crud.Infrastructure;
using Clearly.Crud.JsonLd;
using Clearly.Crud.EntityGraph;
using Clearly.Crud.RestApi.Infrastructure;
using Clearly.Crud.Services;
using Clearly.EntityRepository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Routing;
using AutoMapper;
using System.Text.Json.Serialization;

namespace Clearly.Crud.RestApi;

/// <summary>
/// A collection of extensions for <see cref="IServiceCollection"/>.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds the required services for Clearly CRUD.
    /// </summary>
    /// <param name="services">The service collection to register services in.</param>
    /// <returns>The passed service collection.</returns>
    public static IServiceCollection AddCrudServices(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services, nameof(services));

        return services
            .AddCoreModules()
            .AddSingleton<IEntityDefinitionGraphFactory, EntityDefinitionGraphFactory>()
            .AddSingleton<IEntityDefinitionGraphMapper, EntityDefinitionGraphMapper>()
            .AddScoped(typeof(IEntityApiService<>), typeof(LocalEntityApiService<>))
            .AddScoped(typeof(ICrudService<>), typeof(CrudEntityService<>))
            .AddScoped(typeof(EntityDataSource<>))
            // .AddSingleton(typeof(IRouteMapper<,>), typeof(RouteMapper<,>))
            // .AddSingleton(typeof(IRouteHandler<,>), typeof(RouteHandler<,>))
            .AddSingleton(typeof(RouteMapper<,,>))
            .AddScoped(typeof(RouteHandler<,>))
            .AddSingleton<IEntityDtoCompiler, EntityDtoCompiler>()
            .AddSingleton<IEntityReferenceTypeCompiler, EntityReferenceTypeCompiler>()
            .AddScoped<EntityDtoMapperProfile>()
            .AddScoped<IMapper>(x => new Mapper(CreateMapperConfiguration(x)));
    }

    private static AutoMapper.IConfigurationProvider CreateMapperConfiguration(IServiceProvider services)
    {
        return new MapperConfiguration(y => y.AddProfile(services.GetRequiredService<EntityDtoMapperProfile>()));
    }

    /// <summary>
    /// Add a local in memory entity repository that cleared when the service shuts down.
    /// </summary>
    /// <param name="services">The service collection to register services in.</param>
    /// <remarks>
    /// This is intended for use in testing.
    /// </remarks>
    /// <returns>The passed service collection.</returns>
    public static IServiceCollection AddInMemoryEntityRepository(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services, nameof(services));

        return services
            .AddScoped(typeof(IEntityRepository<>), typeof(LocalMemoryEntityRepository<>));
    }

    /// <summary>
    /// Add route contentions required for Clearly CRUD.
    /// </summary>
    /// <param name="options">The MVC options to modify.</param>
    /// <returns>The passed options.</returns>
    public static MvcOptions AddCrudConvention(this MvcOptions options)
    {
        ArgumentNullException.ThrowIfNull(options, nameof(options));

        options.Conventions.Add(new GenericControllerRouteConvention());

        return options;
    }

    /// <summary>
    /// Adds required MVC Features for Clearly CRUD
    /// </summary>
    /// <param name="builder">The MVC builder to modify.</param>
    /// <param name="assemblies">The assemblies to scan for entities.</param>
    /// <returns>The passed builder.</returns>
    public static IMvcBuilder AddCrudFeature(this IMvcBuilder builder, params Assembly[] assemblies)
    {
        ArgumentNullException.ThrowIfNull(builder, nameof(builder));

        return builder
            .AddCrudFeature(new EntitiesInAssemblyProvider(assemblies));
    }

    /// <summary>
    /// Configures a named <see cref="JsonOptions"/> for the specified <paramref name="builder"/>.
    /// </summary>
    /// <param name="builder">The <see cref="IMvcBuilder"/> to modify.</param>
    /// <param name="name">Name of the <see cref="JsonOptions"/> to configure.</param>
    /// <param name="configure">An <see cref="Action"/> to configure the <see cref="JsonOptions"/>.</param>
    /// <returns>The passed in <see cref="IMvcBuilder"/>.</returns>
    public static IMvcBuilder AddJsonOptions(this IMvcBuilder builder, string name, Action<JsonOptions> configure)
    {
        ArgumentNullException.ThrowIfNull(builder, nameof(builder));
        ArgumentNullException.ThrowIfNull(name, nameof(name));
        ArgumentNullException.ThrowIfNull(configure, nameof(configure));

        builder.Services.Configure(name, configure);
        return builder;
    }

    // TODO: This needs a lot of clean up
    public static IEndpointRouteBuilder MapEntityCrud(this IEndpointRouteBuilder app, Action<RouteHandlerBuilder> configure)
    {
        var typeProvider = app.ServiceProvider.GetRequiredService<ITypeProvider>();
        var definitionGraphFactory = app.ServiceProvider.GetRequiredService<IEntityDefinitionGraphFactory>();

        var typeDefinitions = typeProvider.GetTypes().Select(x => definitionGraphFactory.CreateForType(x));

        foreach (var typeDefinition in typeDefinitions)
        {
            var feature = typeDefinition.Using<CrudApiFeature>();

            if (feature.DtoType == null)
            {
                // TODO: Better Exceptions
                throw new Exception($"No CRUD API DTO Type is defined for {typeDefinition.NodeKey}!");
            }
            if (feature.RefType == null)
            {
                // TODO: Better Exceptions
                throw new Exception($"No CRUD API Ref Type is defined for {typeDefinition.NodeKey}!");
            }

            var routeMapperType = typeof(RouteMapper<,,>).MakeGenericType(feature.DtoType, feature.RefType, typeDefinition.Type);
            var routeMapper = app.ServiceProvider.GetRequiredService(routeMapperType);

            var mappingMethod = routeMapperType.GetMethod("MapEntities");

            mappingMethod?.Invoke(routeMapper, new object[] { app, configure });
        }

        return app;
    }

    public static IEndpointRouteBuilder MapEntityCrud(this IEndpointRouteBuilder app)
    {
        return MapEntityCrud(app, x => x
            .WithMetadata());
    }

    /// <summary>
    /// Adds the CRUD Exception Handler middleware for handling many known exceptions with their
    /// appropriate status codes
    /// </summary>
    /// <remarks>
    /// This should always come before your call to MapControllers()
    /// </remarks>
    public static IApplicationBuilder UseCrudErrorHandler(this IApplicationBuilder app)
    {
        ArgumentNullException.ThrowIfNull(app, nameof(app));

        return app.UseExceptionHandler(x => x.Run(ExceptionHandler.HandleCrudApiException));
    }

    /// <summary>
    /// Adds all the required service and initializes MVC Controllers for a Clearly CRUD API
    /// </summary>
    /// <param name="assemblies">Assemblies containing Entity definitions.</param>
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
    /// <param name="assemblies">Assemblies containing Entity definitions.</param>
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

        // TODO: I hate this, it feels so against how aspnetcore wants you to setup Formatters, but we need EntityDefinitions in our formatter
        // Look into ways to rework this so that the DI container doesn't need to be fully initialized when we create our Formatter
        services.AddScoped(x => 
            new SystemTextJsonLdOutputFormatter(
                x.GetRequiredService<IOptionsSnapshot<JsonOptions>>().Get("jsonld").JsonSerializerOptions, 
                x.GetRequiredService<JsonLdObjectConverterFactory>()));
        services.AddScoped(x => 
            new SystemTextJsonOutputFormatter(
                x.GetRequiredService<IOptions<JsonOptions>>().Value.JsonSerializerOptions));

        services.Configure<JsonOptions>(x => x.JsonSerializerOptions.Converters.Add(new JTokenJsonConverter()));
        services.Configure<Microsoft.AspNetCore.Mvc.JsonOptions>(x => x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
        services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(x => x.SerializerOptions.Converters.Add(new JsonStringEnumConverter()));

        return services
            .AddCrudServices();
    }

    private static IServiceCollection AddCoreModules(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services, nameof(services));

        return services
            .AddSingleton<IDefinitionNodeModule, AttributeBasedEntityFieldModule>()
            .AddSingleton<IDefinitionNodeModule, AttributeBasedEntityModule>()
            .AddSingleton<IDefinitionNodeModule, CoreEntityFieldModule>()
            .AddSingleton<IDefinitionNodeModule, CoreEntityModule>()
            .AddSingleton<IDefinitionNodeModule, CrudApiModule>();
    }

    private static IMvcBuilder AddCrudFeature(this IMvcBuilder builder, ITypeProvider typeProvider)
    {
        builder.Services.AddSingleton(typeProvider);

        // return builder
        //     .ConfigureApplicationPartManager(x =>
        //     {
        //         if (!x.FeatureProviders.Any(y => y is GenericControllerFeatureProvider))
        //         {
        //             x.FeatureProviders.Add(new GenericControllerFeatureProvider(typeProvider));
        //         }
        //     });

        return builder;
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
