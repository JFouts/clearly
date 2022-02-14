using System.Reflection;
using DomainModeling.Crud.WebUi.Factories;
using DomainModeling.Crud.WebUi.Infrastructure;
using DomainModeling.Crud.WebUi.ViewComponents.FieldEditors;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.DependencyInjection;

namespace DomainModeling.Crud.WebUi;

public static class ServiceCollectionExtensions
{
    public static IMvcBuilder AddCrudWebUi(this IServiceCollection services, params Assembly[] assemblies)
    {   

        return services
            .AddCrudWebUIServices(assemblies)
            .Configure<RazorViewEngineOptions>(options => options.ViewLocationExpanders.Add(new GenericControllerViewLocationExpander()))
            .AddCrudServices()
            .AddInMemoryEntityRepository() // TODO: Remove this when we can
            .AddCrudMvc(assemblies);        
    }

    public static IServiceCollection AddCrudWebUIServices(this IServiceCollection services, params Assembly[] assemblies)
    {
        services.AddScoped<IFieldDefinitionFactory, DefaultFieldDefinitionFactory>();
        services.AddScoped(typeof(IListViewModelFactory<>), typeof(ListViewModelFactory<>));
        services.AddScoped(typeof(IDataSourceReader<>), typeof(AutoMapperDataSourceReader<>));
        services.AddScoped<IDataSourceFactory, DataSourceFactory>();
        services.AddAutoMapper(assemblies.Union(new [] { typeof(ServiceCollectionExtensions).Assembly }));

        return services;
    }

    public static IMvcBuilder AddCrudMvc(this IServiceCollection services, params Assembly[] assemblies)
    {
        return services
            .AddMvc(x => 
                x.AddCrudConvention())
            .AddCrudFeature(assemblies);
    }
}
