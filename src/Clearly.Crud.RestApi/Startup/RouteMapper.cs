// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Clearly.Crud.EntityGraph;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Clearly.Core;

namespace Clearly.Crud.RestApi;

public class RouteMapper<TDto, TRef, TEntity>
    where TEntity : class, IEntity, new()
    where TRef : class, IEntity, new()
{
    private readonly IEntityDefinitionGraphFactory entityDefinitionGraphFactory;

    public RouteMapper(IEntityDefinitionGraphFactory entityDefinitionGraphFactory)
    {
        this.entityDefinitionGraphFactory = entityDefinitionGraphFactory;
    }

    public IEndpointRouteBuilder MapEntities(IEndpointRouteBuilder app, Action<RouteHandlerBuilder> configure)
    {
        var definition = entityDefinitionGraphFactory.CreateForObjectType<TEntity>();

        configure(app.MapGet($"api/{definition.NodeKey}", 
            ([FromQuery] string? query,
            [FromQuery] int? skip,
            [FromQuery] int? take,
            [FromServices] RouteHandler<TDto, TRef> handler) => 
                handler.Search(query ?? string.Empty, skip ?? 0, take ?? 24)));

        configure(app.MapGet($"api/{definition.NodeKey}/{{id}}", 
            ([FromRoute] Guid id,
            [FromServices] RouteHandler<TDto, TRef> handler) => 
                handler.Get(id)));

        configure(app.MapPost($"api/{definition.NodeKey}", 
            ([FromBody]TDto value,
            [FromServices] RouteHandler<TDto, TRef> handler) => 
                handler.Post(value)));

        configure(app.MapPut($"api/{definition.NodeKey}/{{id}}", 
            ([FromRoute] Guid id,
            [FromBody]TDto value,
            [FromServices] RouteHandler<TDto, TRef> handler) => 
                handler.Put(id, value)));

        configure(app.MapDelete($"api/{definition.NodeKey}/{{id}}", 
            ([FromRoute] Guid id,
            [FromServices] RouteHandler<TDto, TRef> handler) => 
                handler.Delete(id)));
        
        return app;
    }
}
