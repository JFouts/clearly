// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Reflection;
using Clearly.Crud.RestApi;
using Clearly.Crud.WebUi.Core;
using Clearly.Crud.WebUi.Core.Services;
using Clearly.Crud.WebUi.Factories;
using Clearly.Crud.WebUi.Infrastructure;
using Clearly.Crud.WebUi.ViewComponents.FieldEditors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.DependencyInjection;

namespace Clearly.Crud.WebUi;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds all the required service and initializes MVC for a Clearly Web UI
    /// </summary>
    /// <param name="services">The service collection to register the Web UI in.</param>
    /// <param name="assemblies">Assemblies containing Entity defintions.</param>
    /// <returns><see cref="IMvcBuilder"/> from <see cref="MvcServiceCollectionExtensions.AddMvc"/>.</returns>
    public static IMvcBuilder AddCrudWebUi(this IServiceCollection services, params Assembly[] assemblies)
    {
        return services
            .AddCrudWebUIServices(assemblies)
            .AddCrudWebUiInternal(null, assemblies);
    }

    /// <summary>
    /// Adds all the required service and initializes MVC for a Clearly Web UI.
    /// </summary>
    /// <param name="services">The service collection to register the Web UI in.</param>
    /// <param name="configure">Configuration function for <see cref="MvcOptions"/>.</param>
    /// <param name="assemblies">Assemblies containing Entity defintions.</param>
    /// <returns><see cref="IMvcBuilder"/> from <see cref="MvcServiceCollectionExtensions.AddMvc"/>.</returns>
    public static IMvcBuilder AddCrudWebUi(this IServiceCollection services, Action<MvcOptions> configure, params Assembly[] assemblies)
    {
        return services
            .AddCrudWebUIServices(assemblies)
            .AddCrudWebUiInternal(configure, assemblies);
    }

    /// <summary>
    /// Adds the required services for Clearly CRUD's Web UI.
    /// </summary>
    /// <remarks>
    /// Typically you should use AddCrudWebUi. Call this method only if you are manually configuring MVC for example if you are running
    /// the Clearly Web UI side by side with another Web Application or API.
    /// </remarks>
    /// <param name="services">The service collection to register the Web UI in.</param>
    /// <param name="assemblies">The assembiles to scan for Entites in.</param>
    /// <returns>The passed in service collection.</returns>
    public static IServiceCollection AddCrudWebUIServices(this IServiceCollection services, params Assembly[] assemblies)
    {
        // TODO: Consolidate this into a single registration for a module
        services.AddSingleton<IDefinitionNodeModule, CrudAdminModule>();

        services.AddScoped(typeof(IEntityEditorViewModelFactory<>), typeof(EntityEditorViewModelFactory<>));
        services.AddScoped(typeof(ISearchListViewModelFactory<>), typeof(SearchListViewModelFactory<>));
        services.AddScoped(typeof(IDataSourceReader<>), typeof(AutoMapperDataSourceReader<>));
        // services.AddScoped<IDataSourceFactory, DataSourceFactory>();
        
        // TODO: These should not be needed server side
        // services.AddScoped(sp => new HttpClient());
        // services.AddScoped<IEntityApiService, EntityApiService>();
        // services.AddScoped<IEntityDefinitionApiService, EntityDefinitionApiService>();

        services.AddAutoMapper(assemblies.Union(new[] { typeof(ServiceCollectionExtensions).Assembly }));

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
