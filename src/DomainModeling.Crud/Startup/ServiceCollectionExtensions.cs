using System.Reflection;
using DomainModeling.Crud.Infrastructure;
using DomainModeling.Crud.Services;
using DomainModeling.EntityRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace DomainModeling.Crud;

public static class ServiceCollectionExtensions
{
    public static IMvcBuilder AddCrud(this IServiceCollection services, params Assembly[] assemblies)
    {   
        return services
            .AddCrudServices()
            .AddInMemoryEntityRepository()
            .AddCrudMvc(assemblies);
    }

    public static IServiceCollection AddCrudServices(this IServiceCollection services)
    {
        return services.AddScoped(typeof(IEntityRepository<>), typeof(LocalMemoryEntityRepository<>));
    }

    public static IServiceCollection AddInMemoryEntityRepository(this IServiceCollection services)
    {
        return services.AddScoped(typeof(ICrudService<>), typeof(CrudEntityService<>));
    }

    public static IMvcBuilder AddCrudControllers(this IServiceCollection services, params Assembly[] assemblies)
    {
        return services
            .AddControllers(x => 
                x.AddCrudConvention())
            .AddCrudFeature(assemblies);
    }
    
    public static IMvcBuilder AddCrudMvc(this IServiceCollection services, params Assembly[] assemblies)
    {
        return services
            .AddMvc(x => 
                x.AddCrudConvention())
            .AddCrudFeature(assemblies);
    }
    
    public static MvcOptions AddCrudConvention(this MvcOptions options)
    {
        options.Conventions.Add(new GenericControllerRouteConvention());
        options.Conventions.Add(new GenericActionRouteConvention());
        
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
                x.FeatureProviders.Add(new EntityCrudControllerFeatureProvider(typeProvider)));
    }
}
