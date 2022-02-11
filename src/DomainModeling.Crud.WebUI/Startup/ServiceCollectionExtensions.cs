using System.Reflection;
using DomainModeling.Crud.WebUi.Factories;
using DomainModeling.Crud.WebUi.Infrastructure;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.DependencyInjection;

namespace DomainModeling.Crud.WebUi;

public static class ServiceCollectionExtensions
{
    public static IMvcBuilder AddCrudWithUi(this IServiceCollection services, params Assembly[] assemblies)
    {   
        services.AddScoped<IFieldDefinitionFactory, DefaultFieldDefinitionFactory>();
        services.AddScoped(typeof(IListViewModelFactory<>), typeof(ListViewModelFactory<>));

        return services
            .Configure<RazorViewEngineOptions>(
                options => options.ViewLocationExpanders.Add(new GenericControllerViewLocationExpander()))
            .AddCrud(assemblies);        
    }
}
