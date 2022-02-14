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
            .AddScoped(typeof(IEntityApiService<>), typeof(LocalEntityApiService<>))
            .AddScoped(typeof(EntityDataSource<>))
            .AddScoped(typeof(IEntityRepository<>), typeof(LocalMemoryEntityRepository<>));
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
                x.FeatureProviders.Add(new GenericControllerFeatureProvider(typeProvider)));
    }
}
