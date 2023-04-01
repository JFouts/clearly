// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Clearly.Core;
using Clearly.Crud.Search;
using Microsoft.AspNetCore.Http;
using AutoMapper;

namespace Clearly.Crud.RestApi;

public class RouteHandler<TDto, TEntity> where TEntity : class, IEntity, new()
{
    private readonly ICrudService<TEntity> service;
    private readonly IMapper mapper;

    public RouteHandler(ICrudService<TEntity> service, IMapper mapper)
    {
        this.service = service;
        this.mapper = mapper;
    }

    public async Task<IResult> Search(string query, int skip, int take)
    {
        return Results.Ok(await service.Search(new CrudSearchOptions
        {
            SearchQuery = query,
            Skip = skip,
            Take = take,
        }));
    }
    
    // TODO: [JsonLdFormatter]
    public async Task<IResult> Get(Guid id)
    {
        var entity = await service.GetById(id);

        return Results.Ok(mapper.Map<TDto>(entity));
    }

    public async Task<IResult> Post(TDto value)
    {
        if (value is null)
        {
            return Results.BadRequest("value is required");
        }

        await service.Insert(mapper.Map<TEntity>(value));

        // TODO: Return some reference to the inserted record
        return Results.Ok();
    }

    public async Task<IResult> Put(Guid id, TDto value)
    {
        await service.Update(id, mapper.Map<TEntity>(value));

        // TODO: Return some reference to the inserted record
        return Results.Ok();
    }

    public async Task<IResult> Delete(Guid id)
    {
        await service.Delete(id);

        return Results.Ok();
    }
}
