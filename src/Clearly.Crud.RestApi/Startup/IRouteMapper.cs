// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Clearly.Core;

namespace Clearly.Crud.RestApi;

public interface IRouteMapper<TDto, TEntity> where TEntity : class, IEntity, new()
{
    IEndpointRouteBuilder MapEntities(IEndpointRouteBuilder app, Action<RouteHandlerBuilder> configure);
}
