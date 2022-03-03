// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Reflection;
using DomainModeling.Crud.WebUi.Factories;
using DomainModeling.Crud.WebUi.Infrastructure;
using DomainModeling.Crud.WebUi.ViewComponents.FieldEditors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.DependencyInjection;

namespace DomainModeling.Crud.WebUi;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds all the required service and initializes MVC for a Clearly Web UI
    /// </summary>
    /// <param name="assemblies">Assemblies containing Entity defintions.</param>
    /// <returns>IMvcBuilder from AddMvc()</returns>
    public static IMvcBuilder AddCrudWebUi(this IServiceCollection services, params Assembly[] assemblies)
    {
        return services
            .AddCrudWebUIServices(assemblies)
            .AddCrudWebUiInternal(null, assemblies);
    }

    /// <summary>
    /// Adds all the required service and initializes MVC for a Clearly Web UI
    /// </summary>
    /// <param name="assemblies">Assemblies containing Entity defintions.</param>
    /// <returns>IMvcBuilder from AddMvc()</returns>
    public static IMvcBuilder AddCrudWebUi(this IServiceCollection services, Action<MvcOptions> configure, params Assembly[] assemblies)
    {
        return services
            .AddCrudWebUIServices(assemblies)
            .AddCrudWebUiInternal(configure, assemblies);
    }

    /// <summary>
    /// Adds the required services for Clearly CRUD's Web UI
    /// </summary>
    /// <remarks>
    /// Typically you should use AddCrudWebUi. Call this method only if you are manually configuring MVC for example if you are running
    /// the Clearly Web UI side by side with another Web Application or API.
    /// </remarks>
    public static IServiceCollection AddCrudWebUIServices(this IServiceCollection services, params Assembly[] assemblies)
    {
        services.AddSingleton<IEntityModule, CrudAdminEntityModule>();
        services.AddSingleton<IEntityFieldModule, CrudAdminEntityFieldModule>();

        services.AddScoped(typeof(IEntityEditorViewModelFactory<>), typeof(EntityEditorViewModelFactory<>));
        services.AddScoped(typeof(ISearchListViewModelFactory<>), typeof(SearchListViewModelFactory<>));
        services.AddScoped(typeof(IDataSourceReader<>), typeof(AutoMapperDataSourceReader<>));
        services.AddScoped<IDataSourceFactory, DataSourceFactory>();
        services.AddAutoMapper(assemblies.Union(new [] { typeof(ServiceCollectionExtensions).Assembly }));

        services
            .Configure<RazorViewEngineOptions>(options => options.ViewLocationExpanders.Add(new GenericControllerViewLocationExpander()));

        return services.AddCrudServices();
    }

    private static IMvcBuilder AddCrudWebUiInternal(this IServiceCollection services, Action<MvcOptions>? configure, params Assembly[] assemblies)
    {
        return services
            .AddMvc(options =>
            {
                options.AddCrudConvention();
                if (configure != null)
                {
                    configure(options);
                }
            })
            .AddCrudFeature(assemblies);
    }
}
