// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Reflection;
using DomainModeling.Crud.RestApi.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace DomainModeling.Crud.RestApi;

public static class ServiceCollectionExtensions
{
    public static IMvcBuilder AddCrudRestApi(this IServiceCollection services, params Assembly[] assemblies)
    {
        return services.AddCrudRestApi(NullAction<MvcOptions>.NoOp(), assemblies);
    }

    public static IMvcBuilder AddCrudRestApi(this IServiceCollection services, Action<MvcOptions> config, params Assembly[] assemblies)
    {
        return services
            .AddCrudServices()
            .AddInMemoryEntityRepository() // TODO: Replace this
            .AddControllers(o =>
            {
                o.AddCrudConvention();
                config(o);
            })
            .AddCrudFeature(assemblies);
    }

    // Option 1 - I only want to use the CRUD services/repositories
    // services
    //     .AddCrudServices()
    //     .AddInMemoryEntityRepository(); // Some Entity Repository Implementation

    // Option 2 - I want to use the CRUD REST API standalone
    // services.AddCrudRestApi();

    // Option 3 - I want to run the CRUD REST API in the same app a Razor/Views application
    // services
    //     .AddCrudServices()
    //     .AddInMemoryEntityRepository(); // Some Entity Repository Implementation
    //
    // services
    //     .AddMvc(o =>   // Or AddControllersWithViews
    //          o.AddCrudConvention())
    //     .AddCrudFeatures();

    // Option 4 - I want to run the CRUD REST API along side the CRUD Web UI
    // TODO

    // Option 5 - I want to run the CRUD Web UI standalone
    // TODO
}
