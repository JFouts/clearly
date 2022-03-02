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
    public static IServiceCollection AddCrudServices(this IServiceCollection services)
    {
        return services
            .AddCoreModules()
            .AddSingleton<IEntityDefinitionFactory, EntityDefinitionFactory>()
            .AddScoped(typeof(IEntityApiService<>), typeof(LocalEntityApiService<>))
            .AddScoped(typeof(EntityDataSource<>))
            .AddScoped(typeof(IEntityRepository<>), typeof(LocalMemoryEntityRepository<>));
    }

    private static IServiceCollection AddCoreModules(this IServiceCollection services)
    {
        return  services
            .AddSingleton<IEntityFieldModule, AttributeBasedEntityFieldModule>()
            .AddSingleton<IEntityModule, AttributeBasedEntityModule>()
            .AddSingleton<IEntityFieldModule, CoreEntityFieldModule>()
            .AddSingleton<IEntityModule, CoreEntityModule>();
    }

    public static IServiceCollection AddInMemoryEntityRepository(this IServiceCollection services)
    {
        return services.AddScoped(typeof(ICrudService<>), typeof(CrudEntityService<>));
    }

    public static MvcOptions AddCrudConvention(this MvcOptions options)
    {
        options.Conventions.Add(new GenericControllerRouteConvention());

        return options;
    }

    public static IMvcBuilder AddCrudFeature(this IMvcBuilder builder, params Assembly[] assemblies)
    {
        return builder
            .AddCrudFeature(new EntitiesInAssemblyProvider(assemblies));
    }

    internal static IMvcBuilder AddCrudFeature(this IMvcBuilder builder, ITypeProvider typeProvider)
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
