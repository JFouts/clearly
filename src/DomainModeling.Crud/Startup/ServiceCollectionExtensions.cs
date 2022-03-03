// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Reflection;
using DomainModeling.Crud.Infrastructure;
using DomainModeling.Crud.Services;
using DomainModeling.EntityRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace DomainModeling.Crud;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds the required services for Clearly CRUD
    /// </summary>
    public static IServiceCollection AddCrudServices(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services, nameof(services));

        return services
            .AddCoreModules()
            .AddSingleton<IEntityDefinitionFactory, EntityDefinitionFactory>()
            .AddScoped(typeof(IEntityApiService<>), typeof(LocalEntityApiService<>))
            .AddScoped(typeof(ICrudService<>), typeof(CrudEntityService<>))
            .AddScoped(typeof(EntityDataSource<>));
    }

    /// <summary>
    /// Add a local in memory entity repository that cleared when the service shuts down
    /// </summary>
    /// <remarks>
    /// This is intended for use in testing
    /// </remarks>
    public static IServiceCollection AddInMemoryEntityRepository(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services, nameof(services));

        return services
            .AddScoped(typeof(IEntityRepository<>), typeof(LocalMemoryEntityRepository<>));
    }

    /// <summary>
    /// Add route contentions required for Clearly CRUD
    /// </summary>
    /// <param name="options"></param>
    /// <returns></returns>
    public static MvcOptions AddCrudConvention(this MvcOptions options)
    {
        ArgumentNullException.ThrowIfNull(options, nameof(options));

        options.Conventions.Add(new GenericControllerRouteConvention());

        return options;
    }

    /// <summary>
    /// Adds required MVC Features for Clearly CRUD
    /// </summary>
    public static IMvcBuilder AddCrudFeature(this IMvcBuilder builder, params Assembly[] assemblies)
    {
        ArgumentNullException.ThrowIfNull(builder, nameof(builder));

        return builder
            .AddCrudFeature(new EntitiesInAssemblyProvider(assemblies));
    }

    private static IServiceCollection AddCoreModules(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services, nameof(services));
        
        return  services
            .AddSingleton<IEntityFieldModule, AttributeBasedEntityFieldModule>()
            .AddSingleton<IEntityModule, AttributeBasedEntityModule>()
            .AddSingleton<IEntityFieldModule, CoreEntityFieldModule>()
            .AddSingleton<IEntityModule, CoreEntityModule>();
    }

    private static IMvcBuilder AddCrudFeature(this IMvcBuilder builder, ITypeProvider typeProvider)
    {
        return builder
            .ConfigureApplicationPartManager(x =>
            {
                if (!x.FeatureProviders.Any(y => y is GenericControllerFeatureProvider))
                {
                    x.FeatureProviders.Add(new GenericControllerFeatureProvider(typeProvider));
                }
            });
    }
}
