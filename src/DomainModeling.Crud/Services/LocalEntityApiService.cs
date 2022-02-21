// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using DomainModeling.Core;
using DomainModeling.Crud.Search;

namespace DomainModeling.Crud.Services;

// TODO: Well defined exceptions that come from this class
public class LocalEntityApiService<TEntity> : IEntityApiService<TEntity>
    where TEntity : IEntity
{
    private readonly ICrudService<TEntity> service;

    public LocalEntityApiService(ICrudService<TEntity> service)
    {
        this.service = service;
    }

    public async Task<CrudSearchResult<TEntity>> Search(CrudSearchOptions searchOptions)
    {
        return await service.Search(searchOptions);
    }

    public async Task<TEntity> GetById(Guid id)
    {
        return await service.GetById(id);
    }

    public async Task Create(TEntity value)
    {
        await service.Insert(value);
    }

    public async Task Update(Guid id, TEntity value)
    {
        await service.Update(id, value);
    }

    public async Task Delete(Guid id)
    {
        await service.Delete(id);
    }
}
